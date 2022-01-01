using System;
using MentolProvision.Auth;
using MentolProvision.Models.Request;
using MentolProvisionModel;
using System.Collections.Generic;
using System.Linq;
using Version = MentolProvisionModel.Version;

namespace MentolProvision.Extensions
{
	public static class ServerPostRequestExtensions
	{
		public static Server ToServer(this ServerPostRequest request)
		{
			if (request == null)
				return null;

			return new Server
			{
				FQDN = request.ServerFQDN,
				IsDeleted = false,
				IpAddress = request.ServerIpAddress,
				Port = request.ServerPort,
				Login = request.ServerLogin,
				Password = request.ServerPassword is null ? null : CryptoManager.AesEncrypt(request.ServerPassword),
				Description = request.ServerDescription,
				ModelId = request.ServerVendorModelId,
				IsEnabled = request.ServerIsEnabled,
				TestBench = request.ServerIsTest,
				Nodes = new List<Node>(request.Nodes.Select(n => new Node
				{
					FQDN = n.NodeFQDN,
					IpAddress = n.NodeIpAddress,
					Priority = n.NodePriority
				}))
			};
		}

		public static Node ToNode(this AddOrUpdateNodeRequest nodeRequest, int? serverId, bool isNewNode = false)
		{
			if (nodeRequest == null)
				return null;

			var node = new Node
			{
				FQDN = nodeRequest.NodeFQDN,
				IdServer = serverId,
				IpAddress = nodeRequest.NodeIpAddress,
				Priority = nodeRequest.NodePriority,
			};

			if (isNewNode && !string.IsNullOrEmpty(nodeRequest.NodeVersion))
				node.Versions.Add(new Version { DateRecord = DateTime.Now, VersionValue = nodeRequest.NodeVersion});

			return node;
		}

		public static Node ToNode(this NodeDataRequest request, int? serverId, int versionNumber = 1)
		{
			if (request == null)
				return null;

			return new Node
			{
				FQDN = request.NodeFQDN,
				IdServer = serverId,
				IpAddress = request.NodeIpAddress,
				Priority = request.NodePriority,
				Versions = new List<Version>(new[] { new Version {DateRecord = DateTime.Now, IsLastRecord = true, VersionValue = versionNumber.ToString()}})
			};
		}

	}
}