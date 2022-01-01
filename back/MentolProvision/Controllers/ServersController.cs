using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using MentolProvision.Models.Response;
using MentolProvisionInterface;
using MentolProvisionModel;
using System.Linq;
using MentolProvision.Auth;
using MentolProvision.Extensions;
using MentolProvision.Models;
using MentolProvision.Models.Request;
using MentolProvision.Models.Response.Summaries;
using MentolProvisionRepository;
using MentolProvisionModel.Infrastructure;
using MentolProvisionRepository.Filter;
using Serilog;
using Version = MentolProvisionModel.Version;

namespace MentolProvision.Controllers
{
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class ServersController : FilteredController<Server, ServersController, ServerResponse>
	{
		#region General

		private ILogger _logger;
		private IDataInterface _data;
		private readonly IStringLocalizer<ServersController> _localizer;
		private readonly ApplicationContext _context;

		//todo: need create filters for boolean field type
		//ExpressionBuilder.Build(searchPattern, OperatorComparer.Contains, nameof(Server.Enable)),
		//ExpressionBuilder.Build(searchPattern, OperatorComparer.Contains, nameof(Server.TestServerId)),
		internal override Dictionary<string, List<string>> _propNames { get; set; } =
			new Dictionary<string, List<string>>()
			{
				{nameof(Server.Idr), new List<string>() {nameof(Server.Idr)}},
				{nameof(Server.FQDN), new List<string>() {nameof(Server.FQDN)}},
				{nameof(Server.Description), new List<string>() {nameof(Server.Description)}},
				{nameof(Server.IpAddress), new List<string>() {nameof(Server.IpAddress)}},
				{nameof(Server.Login), new List<string>() {nameof(Server.Login)}},
				{nameof(Server.Password), new List<string>() {nameof(Server.Password)}},
				{nameof(Server.Mode), new List<string>() {nameof(Server.Mode)}},
				{nameof(Server.Port), new List<string>() {nameof(Server.Port)}},
				{nameof(Server.ModelId), new List<string>() {nameof(Server.ModelId)}},
			};

		public ServersController(ILogger logger, IDataInterface data, IStringLocalizer<ServersController> localizer,
			ApplicationContext context)
			: base(logger, localizer, context)
		{
			_logger = logger;
			_data = data;
			_localizer = localizer;
			_context = context;
		}

		#endregion

		#region End-points

		public override async Task<IActionResult> GetFilteredAsync(FilterRequest request)
		{
			try
			{
				if (request?.Offset == null || request.Limit == null)
					return BadRequest(_localizer["ERROR_NEED_LIMIT_OFFSET"].Value);


				var data = await ((Repository) _data).GetServerRowsFromProcedure(request);
				var result = data.Select(x => new ServerResponse()
				{
					ServerId = x.ServerId,
					ServerFQDN = x.ServerFQDN,
					ServerIpAddress = x.ServerIpAddress,
					ServerPort = x.ServerPort,
					ServerLogin = x.ServerLogin,
					ServerDescription = x.ServerDescription,
					ServerVendorModelId = x.ServerVendorModelId,
					ServerVendorName = x.ServerVendorName,
					ServerIsEnabled = x.ServerIsEnabled,
					ServerIsTest = x.ServerIsTest,
					NodeId = x.NodeId,
				}).ToList();

				var ids = data.Where(x => !string.IsNullOrEmpty(x.NodeId)).Select(x => x.ServerId).ToArray();

				var nodes = await _data.GetNodesAsync(n => ids.Contains(n.IdServer.Value));

				foreach (var sr in result)
				{
					if (!string.IsNullOrEmpty(sr.NodeId))
					{
						sr.Nodes.AddRange(nodes.Where(x => x.IdServer == sr.ServerId).Select(x => x.ToNodeResponse()));
					}
				}

				return Ok(GetSelectionResponse(result, request));
			}
			catch (Exception ex) 
			{
				 _logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}


		public override async Task<IActionResult> GetFilteredCountAsync(FilterCountRequest request)
		{
			try
			{
				var data = await ((Repository)_data).GetServersCountFromProcedureAsync(request);
				return Ok(GetCountResponse(data, request));
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Получить детальную информацию по конкретному серверу, включая ноды
		/// </summary>
		/// <param name="serverId">Идентификатор сервера</param>
		/// <returns></returns>
		[ProducesResponseType(typeof(List<Server>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpGet("{serverId}")]
		public async Task<IActionResult> GetDetails(int serverId)
		{
			try
			{
				var server = await _data.GetServerAsync(serverId);
				if (server == null)
					return NotFound();

				return Ok(server.ToDetailServerResponse());
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_GET"].Value + ex.Message);
				return StatusCode(500);
			}
		}



		/// <summary>
		/// Добавить сервер
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(Server), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ServerPostRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				if (request?.ServerFQDN == null)
					return BadRequest(_localizer["ERROR_NEED_NAME"].Value);

				var result = ValidateNodesBatch(request.Nodes);
				if (!result.IsSucceeded)
				{
					return StatusCode(StatusCodes.Status409Conflict, result.Error);
				}

				if (request.ServerVendorModelId.HasValue)
				{
					var model = await _data.GetVendorModelsAsync(request.ServerVendorModelId.Value);
					if (model == null || !model.Any())
						return BadRequest(_localizer["ERROR_METHOD_POST_INCORRECT_MODEL_ID"].Value);
				}

				if (!IsServerUnique(request.ServerFQDN)) 
					return BadRequest(_localizer["ERROR_IS_NOT_UNIQUE_NAME"].Value);

				foreach (var node in request.Nodes)
				{
					if (!string.IsNullOrEmpty(node.NodeFQDN))
					{
						var toFoundNode = await _data.GetSingleNodeAsync(n => node.NodeFQDN.Equals(n.FQDN));

						if (toFoundNode != null)
							return BadRequest(_localizer["ERROR_IS_NOT_UNIQUE_NODE_FQDN", node.NodeFQDN].Value);
					}

					if (!string.IsNullOrEmpty(node.NodeIpAddress))
					{
						var toFoundNode = await _data.GetSingleNodeAsync(n => node.NodeIpAddress.Equals(n.IpAddress));

						if (toFoundNode != null)
							return BadRequest((_localizer["ERROR_IS_NOT_UNIQUE_NODE_IP_ADDRESS", node.NodeIpAddress].Value));
					}
				}

				var summary = new ServerSummaryResponse();

				var newServer = request.ToServer();
				await _data.AddServerAsync(newServer);
				summary.Status.Result = "success";
				summary.Data = new ServerResponse(newServer);
				return CreatedAtAction(nameof(Post), summary);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_POST"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Добавить узлы сервера
		/// </summary>
		/// <param name="serverId">server identifier</param>
		/// <param name="request">Nodes data</param>
		/// <returns></returns>
		[ProducesResponseType(typeof(Server), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost("nodes")]
		public async Task<IActionResult> AddNodes(NodesPostRequest request)
		{
			try
			{
				if (request.ServerId == null)
					return BadRequest(_localizer["ERROR_NEED_SERVER_ID"].Value);

				var serverId = request.ServerId.Value;

				if (request.Nodes.Count == 0)
					return BadRequest(_localizer["ERROR_NO_NODES_TO_ADD_DEFINED"].Value);

				var result = ValidateNodesBatch(request.Nodes);
				if (!result.IsSucceeded)
				{
					return StatusCode(StatusCodes.Status409Conflict, result.Error);
				}

				foreach (var nodeToAdd in request.Nodes)
				{
					if (!string.IsNullOrEmpty(nodeToAdd.NodeFQDN))
					{
						var nodeToFound = await _data.GetSingleNodeAsync(n => nodeToAdd.NodeFQDN.Equals(n.FQDN));
						if (nodeToFound != null)
							return StatusCode(StatusCodes.Status409Conflict, _localizer["ERROR_IS_NOT_UNIQUE_NODE_FQDN", nodeToAdd.NodeFQDN].Value);
					}

					if (!string.IsNullOrEmpty(nodeToAdd.NodeIpAddress))
					{
						var toFoundNode = await _data.GetSingleNodeAsync(n => nodeToAdd.NodeIpAddress.Equals(n.IpAddress));

						if (toFoundNode != null)
							return StatusCode(StatusCodes.Status409Conflict , _localizer["ERROR_IS_NOT_UNIQUE_NODE_IP_ADDRESS", nodeToAdd.NodeIpAddress].Value);
					}

					if (nodeToAdd.NodePriority != null)
					{
						var nodeToFound = await _data.GetSingleNodeAsync(n => nodeToAdd.NodePriority.Equals(n.Priority) && n.IdServer == serverId);
						if (nodeToFound != null)
							return StatusCode(StatusCodes.Status409Conflict, _localizer["ERROR_IS_NOT_UNIQUE_NODE_PRIORITY", nodeToAdd.NodePriority, serverId].Value);
					}

					_context.Nodes.Add(nodeToAdd.ToNode(serverId));
				}

				await _context.SaveChangesAsync();
				return CreatedAtAction(nameof(AddNodes), null);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_POST"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpDelete("nodes/{nodeId}")]
		public async Task<IActionResult> DeleteNode(int nodeId)
		{
			try
			{
				var node = await _data.GetSingleNodeAsync(n => n.Idr.Equals(nodeId));
				if (node == null)
					return NotFound();

				_context.Nodes.Remove(node);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (Exception e)
			{
				_logger.Error(e, $"Ошибка удаления узла c Idr={nodeId}: " + e.Message);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		/// <summary>
		/// Удалить сервер
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>             
		[ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteServer(int id)
		{
			try
			{
				var server = await _data.GetServerAsync(id);

				if (server == null) 
					return NotFound();

				await _data.DeleteServerAsync(server);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.Error(ex, _localizer["ERROR_METHOD_DELETE"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		/// <summary>
		/// Изменить информацию о сервере
		/// </summary>
		/// <param name="id"></param>
		/// <param name="server"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] ServerPutRequest request)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				if (request?.ServerId == null)
					return BadRequest(_localizer["ERROR_NEED_ID"].Value);

				var existingServer = await _data.GetServerAsync(request.ServerId.Value);
				if (existingServer == null)
					return NotFound();

				if (request.ServerVendorModelId.HasValue)
				{
					var model = await _data.GetVendorModelsAsync(request.ServerVendorModelId.Value);
					if (model == null || !model.Any())
						return BadRequest(_localizer["ERROR_METHOD_PUT_INCORRECT_MODEL_ID"].Value);
				}

				request.Nodes ??= Enumerable.Empty<AddOrUpdateNodeRequest>().ToList();

				var result = ValidateNodesBatch(request.Nodes);
				if (!result.IsSucceeded)
				{
					return StatusCode(StatusCodes.Status409Conflict, result.Error);
				}

				var serverByName = await _data.GetServerAsync(request.ServerFQDN);
				if (serverByName != null && serverByName.Idr != request.ServerId)
					return BadRequest(_localizer["ERROR_IS_NOT_UNIQUE_NAME"].Value);


				var serverByIpAddress = await _data.GetServerByIpAddress(request.ServerIpAddress);
				 if (serverByIpAddress != null && serverByIpAddress.Idr != request.ServerId)
					 return BadRequest(_localizer["ERROR_IS_NOT_UNIQUE_SERVER_IP_ADDRESS", request.ServerIpAddress].Value);

				existingServer.FQDN = request.ServerFQDN;
				existingServer.Description = request.ServerDescription;
				existingServer.IpAddress = request.ServerIpAddress;
				existingServer.Login = request.ServerLogin;
				existingServer.Password = request.ServerPassword is null ? null : CryptoManager.AesEncrypt(request.ServerPassword);
				existingServer.Port = request.ServerPort;
				existingServer.IsEnabled = request.ServerIsEnabled;
				existingServer.TestBench = request.ServerIsTest;
				existingServer.ModelId = request.ServerVendorModelId;

				var summary = new ServerSummaryResponse();

				//обновляем только на основе тех нод, которые имеют PK в JSON-е
				var nodeIds = request.Nodes.Where(x => x.NodeId.HasValue).Select(x => x.NodeId).ToArray();

				foreach (var requestNode in request.Nodes.Where(x => x.NodeId.HasValue))
				{
					var nodeToUpdate = existingServer.Nodes.FirstOrDefault(x => x.Idr == requestNode.NodeId);

					if (nodeToUpdate == null)
						return BadRequest(_localizer["ERROR_NODE_DOES_NOT_EXIST", existingServer.Idr, requestNode.NodeId].Value);

					if (!string.IsNullOrEmpty(requestNode.NodeFQDN))
					{
						var nodeToFound = await _data.GetSingleNodeAsync(n =>
							requestNode.NodeFQDN.Equals(n.FQDN) && !n.Idr.Equals(requestNode.NodeId));

						if (nodeToFound != null)
							return BadRequest(string.Format(_localizer["ERROR_IS_NOT_UNIQUE_NODE_FQDN", requestNode.NodeFQDN].Value));
					}

					if (!string.IsNullOrEmpty(requestNode.NodeIpAddress))
					{
						var nodeToFound = await _data.GetSingleNodeAsync(n =>
							requestNode.NodeIpAddress.Equals(n.IpAddress) && !n.Idr.Equals(requestNode.NodeId));

						if (nodeToFound != null)
							return BadRequest(string.Format(_localizer["ERROR_IS_NOT_UNIQUE_NODE_IPADDRESS", requestNode.NodeIpAddress].Value));
					}

					if (requestNode.NodePriority != null)
					{
						var toFound = existingServer.Nodes.FirstOrDefault(x => x.Priority == requestNode.NodePriority);
						if (toFound != null)
							return StatusCode(StatusCodes.Status409Conflict, _localizer["ERROR_IS_NOT_UNIQUE_NODE_PRIORITY", requestNode.NodePriority, existingServer.Idr].Value);
					}

					//можно затирать старые значения null-ами
					nodeToUpdate.FQDN = requestNode.NodeFQDN;
					nodeToUpdate.IpAddress = requestNode.NodeIpAddress;
					nodeToUpdate.Priority = requestNode.NodePriority;

					if (!string.IsNullOrEmpty(requestNode.NodeVersion))
					{
						var currentVersion = nodeToUpdate.Versions.SingleOrDefault(x => x.IsLastRecord ?? false);

						var newVersion = new Version { VersionValue = requestNode.NodeVersion, IsLastRecord = true};

						if (currentVersion == null)
						{
							nodeToUpdate.Versions.Add(newVersion);
							continue;
						}

						if (!currentVersion.VersionValue.Equals(requestNode.NodeVersion))
						{
							currentVersion.IsLastRecord = false;
							nodeToUpdate.Versions.Add(newVersion); 
						}
					}
				}

				var newNodes = request.Nodes.Where(x => !x.NodeId.HasValue).ToArray();

				foreach (var newNode in newNodes)
				{
					if (!string.IsNullOrEmpty(newNode.NodeFQDN))
					{
						var nodeToFound = await _data.GetSingleNodeAsync(n =>
							newNode.NodeFQDN.Equals(n.FQDN));

						if (nodeToFound != null)
							return BadRequest(_localizer["ERROR_IS_NOT_UNIQUE_NODE_FQDN", newNode.NodeFQDN].Value);
					}

					if (!string.IsNullOrEmpty(newNode.NodeIpAddress))
					{
						var nodeToFound = await _data.GetSingleNodeAsync(n =>
							newNode.NodeIpAddress.Equals(n.IpAddress));

						if (nodeToFound != null)
							return BadRequest(string.Format(_localizer["ERROR_IS_NOT_UNIQUE_NODE_IPADDRESS", newNode.NodeIpAddress].Value));
					}

					if (newNode.NodePriority != null)
					{
						var toFound = existingServer.Nodes.FirstOrDefault(x => newNode.NodePriority.Equals(x.Priority) && x.IdServer == request.ServerId);
						if (toFound != null)
							return StatusCode(StatusCodes.Status409Conflict, _localizer["ERROR_IS_NOT_UNIQUE_NODE_PRIORITY", newNode.NodePriority, existingServer.Idr].Value);
					}

					var nodeToPersist = newNode.ToNode(request.ServerId, true);
					existingServer.Nodes.Add(nodeToPersist);
				}

				var toDelete = existingServer.Nodes.Where(n => !nodeIds.Contains(n.Idr)).ToArray();

				foreach (var nodeToDelete in toDelete)
					existingServer.Nodes.Remove(nodeToDelete);

				await _data.UpdateServerAsynс(existingServer);
				summary.Data = existingServer.ToServerResponse();
				summary.Status.Result = "success";
				return Ok(summary);
			}
			catch (Exception ex)
			{
				_logger.Error(ex,_localizer["ERROR_METHOD_PUT"].Value + ex.Message);
				return StatusCode(500);
			}
		}

		#endregion

		#region Private filtering methods

		public override Expression<Func<Server, ServerResponse>> GetSelectExpression()
		{
			Expression<Func<Server, ServerResponse>> expression = s => new ServerResponse
			{
				ServerId = s.Idr,
				ServerFQDN = s.FQDN,
				ServerIpAddress = s.IpAddress,
				ServerPort = s.Port,
				ServerLogin = s.Login,
				ServerVendorModelId = s.ModelId,
				ServerDescription = s.Description,
				ServerVendorName = s.VendorModel.Vendor.Name,
				ServerVersion = s.Versions.SingleOrDefault(x => x.IsLastRecord.Value).VersionValue,
				Enable = s.IsEnabled,
				ServerIsTest = s.TestBench ?? false,
				ServerIsEnabled = s.IsEnabled ?? false,
				Nodes = new List<NodeResponse>(s.Nodes.Select(sn => new NodeResponse
				{
					NodeId = sn.Idr,
					NodeFQDN = sn.FQDN,
					NodePriority = sn.Priority,
					NodeVersion = sn.Versions.SingleOrDefault(v => v.IsLastRecord.Value).VersionValue,
					NodeIpAddress = sn.IpAddress
				}))
			};

			return expression;
		}

		public override IOrderedQueryable<ServerResponse> GetOrderExpression(string columnName, bool desc,
			IQueryable<ServerResponse> _query)
		{
			return columnName switch
			{
				nameof(ServerResponse.ServerId) => !desc
					? _query.OrderBy(r => r.ServerId)
					: _query.OrderByDescending(r => r.ServerId),
				nameof(ServerResponse.ServerFQDN) => !desc
					? _query.OrderBy(r => r.ServerFQDN)
					: _query.OrderByDescending(r => r.ServerFQDN),
				nameof(ServerResponse.ServerDescription) => !desc
					? _query.OrderBy(r => r.ServerDescription)
					: _query.OrderByDescending(r => r.ServerDescription),
				nameof(ServerResponse.ServerIpAddress) => !desc
					? _query.OrderBy(r => r.ServerIpAddress)
					: _query.OrderByDescending(r => r.ServerIpAddress),
				nameof(ServerResponse.ServerLogin) => !desc
					? _query.OrderBy(r => r.ServerLogin)
					: _query.OrderByDescending(r => r.ServerLogin),
				nameof(ServerResponse.ServerPort) => !desc
					? _query.OrderBy(r => r.ServerPort)
					: _query.OrderByDescending(r => r.ServerPort),
				nameof(ServerResponse.ServerVendorModelId) => !desc
					? _query.OrderBy(r => r.ServerVendorModelId)
					: _query.OrderByDescending(r => r.ServerVendorModelId),
				nameof(ServerResponse.ServerVersion) => !desc
					? _query.OrderBy(r => r.ServerVersion)
					: _query.OrderByDescending(r => r.ServerVersion),
				nameof(ServerResponse.ServerIsTest) => !desc
					? _query.OrderBy(r => r.ServerIsTest)
					: _query.OrderByDescending(r => r.ServerIsTest),
				nameof(ServerResponse.ServerIsEnabled) => !desc
					? _query.OrderBy(r => r.ServerIsEnabled)
					: _query.OrderByDescending(r => r.ServerIsEnabled),
				nameof(ServerResponse.ServerVendorName) => !desc
					? _query.OrderBy(r => r.ServerVendorName)
					: _query.OrderByDescending(r => r.ServerIsEnabled),
				_ => throw new ArgumentException("invalid order column name", nameof(columnName)),
			};
		}

		#endregion

		#region PrivateFunctions

		private BatchValidationResult ValidateNodesBatch(IEnumerable<NodeDataRequest> nodes)
		{
			if (nodes == null || !nodes.Any())
				return BatchValidationResult.Succeeded;

			var clearFqdnData = nodes.Where(x => !string.IsNullOrEmpty(x.NodeFQDN)).ToArray();

			if (clearFqdnData.GroupBy(x => x.NodeFQDN).Any(g => g.Count() > 1))
				return new BatchValidationResult(_localizer["ERROR_NON_UNIQUE_FQDN_DATA_IN_NODES_BATCH"].Value);

			var clearIpData = nodes.Where(x => !string.IsNullOrEmpty(x.NodeIpAddress)).ToArray();

			if (clearIpData.GroupBy(x => x.NodeIpAddress).Any(g => g.Count() > 1))
				return new BatchValidationResult(_localizer["ERROR_NON_UNIQUE_IP_ADRESS_DATA_IN_NODES_BATCH"].Value);

			var clearPriorityData = nodes.Where(x => x.NodePriority != null).ToArray();

			if (clearPriorityData.GroupBy(x => x.NodePriority).Any(g => g.Count() > 1))
				return new BatchValidationResult(_localizer["ERROR_NON_UNIQUE_PRIORITY_DATA_IN_NODES_BATCH"].Value);

			return BatchValidationResult.Succeeded;
		}

		/// <summary>
		/// Проверить сервер на уникальность имени
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private bool IsServerUnique(string name)
		{
			var server = _data.GetServerAsync(name).Result;

			if (server != null) return false;

			return true;
		}

		public override List<string> GeIncludes()
		{
			return new List<string>
			{
				nameof(Server.Versions),
				nameof(Server.Nodes),
				nameof(Server.VendorModel),
				string.Join(".", nameof(Server.Nodes), nameof(Node.Versions)),
				string.Join(".", nameof(Server.VendorModel), nameof(VendorModel.Vendor))
			};
		}

		protected override object GetSelectionResponse(List<ServerResponse> data, FilterRequest request)
		{
			var summary = new ServersFiltrationSummaryResponse
			{
				Meta =
				{
					Offset = request.Offset,
					Limit = request.Limit,
					OrderDesc = request.OrderDesc ?? true,
					TableColumn = request.TableColumn,
					Search = request.Search
				},
				Servers = data
			};
			return summary;
		}

		protected override object GetCountResponse(int count, FilterCountRequest request)
		{
			return new ServerCountResponse
			{
				Meta =
				{
					Search = request.Search,
					TableColumn = request.TableColumn,
					Count = count
				}
			};
		}

		#endregion
	}
}