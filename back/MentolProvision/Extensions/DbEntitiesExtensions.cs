using System.Collections.Generic;
using System.Linq;
using MentolProvision.Models.Response;
using MentolProvision.Models.Response.Summaries;
using MentolProvisionModel;
using MentolProvisionModel.CustomQueries;

namespace MentolProvision.Extensions
{
	public static class ServerExtensions
	{
		public static ServerResponse ToServerResponse(this Server server)
		{
			if (server == null)
				return null;

			return new ServerResponse
			{
				ServerId = server.Idr,
				ServerFQDN = server.FQDN,
				ServerIpAddress = server.IpAddress,
				ServerPort = server.Port,
				ServerLogin = server.Login,
				ServerVendorModelId = server.ModelId,
				ServerDescription = server.Description,
				ServerVendorName = server.VendorModel?.Vendor.Name,
				ServerVersion = server.Versions.SingleOrDefault(x => x.IsLastRecord ?? false)?.VersionValue,
				Enable = server.IsEnabled,
				ServerIsTest = server.TestBench ?? false,
				ServerIsEnabled = server.IsEnabled ?? false,
				Nodes = new List<NodeResponse>(server.Nodes.Select(sn => new NodeResponse
				{
					NodeId = sn.Idr,
					NodeFQDN = sn.FQDN,
					NodePriority = sn.Priority,
					NodeVersion = sn.Versions.SingleOrDefault(v => v.IsLastRecord ?? false)?.VersionValue,
					NodeIpAddress = sn.IpAddress
				}))
			};
		}

		public static DetailServerResponse ToDetailServerResponse(this Server server)
		{
			if (server == null)
				return null;

			return new DetailServerResponse
			{
				ServerId = server.Idr,
				ServerFQDN = server.FQDN,
				ServerIpAddress = server.IpAddress,
				ServerPort = server.Port,
				ServerLogin = server.Login,
				ServerVendorModelId = server.ModelId,
				ServerDescription = server.Description,
				ServerVendorName = server.VendorModel?.Vendor.Name,
				ServerVersion = server.Versions.SingleOrDefault(x => x.IsLastRecord ?? false)?.VersionValue,
				Enable = server.IsEnabled,
				ServerIsTest = server.TestBench ?? false,
				ServerIsEnabled = server.IsEnabled ?? false,
				ServerDefStartTime = server.DefStartTime,
				Nodes = new List<NodeResponse>(server.Nodes.Select(sn => new NodeResponse
				{
					NodeId = sn.Idr,
					NodeFQDN = sn.FQDN,
					NodePriority = sn.Priority,
					NodeVersion = sn.Versions.SingleOrDefault(v => v.IsLastRecord ?? false)?.VersionValue,
					NodeIpAddress = sn.IpAddress
				}))
			};
		}


		public static CompletedTaskResponse ToCompletedTaskResponse(this CompletedTaskRow row)
		{
			if (row == null)
				return null;

			return new CompletedTaskResponse
			{
				TaskCompletedId = row.TaskCompletedId,
				TaskId = row.TaskId,
				DevicePhoneNumber = row.DevicePhoneNumber,
				TaskType = row.TaskType,
				UserName = row.UserName,
				UserLogin = row.UserLogin,
				TaskDescription = row.TaskDescription,
				TaskDateCreate = row.TaskDateCreate,
				TaskDateRun = row.TaskDateRun,
				TaskDateEnd = row.TaskDateEnd,
				TaskStatus = row.TaskStatus,
				TaskStatusDesc = row.TaskStatusDesc,
				TaskCancelId = row.TaskCancelId,
				ServerTestId = row.ServerTestId,
				ServerFQDN = row.ServerFQDN
			};
		}

		public static RoleResponse ToRoleResponse(this Role role)
		{
			if (role == null)
				return null;

			return new RoleResponse
			{
				RoleId = role.Idr,
				RoleName = role.Name,
				RoleDescription = role.Description
			};
		}

		public static NodeResponse ToNodeResponse(this Node row)
		{
			if (row == null)
				return null;

			return new NodeResponse
			{
				NodeId = row.Idr,
				NodeFQDN = row.FQDN,
				NodeVersion = row.Versions?.FirstOrDefault(x => x.IsLastRecord ?? false)?.VersionValue,
				NodePriority = row.Priority,
				NodeIpAddress = row.IpAddress
			};
		}
	}
}