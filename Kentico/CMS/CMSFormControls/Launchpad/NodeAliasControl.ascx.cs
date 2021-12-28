
using CMS.Base.Web.UI;
using CMS.DocumentEngine;
using CMS.FormEngine.Web.UI;
using CMS.Membership;

public partial class Launchpad_FormControls_NodeAliasControl : FormEngineUserControl
{
	public override object Value
	{
		get
		{
			return TextBox.Text;
		}
		set
		{
			if (value != null)
			{
				TextBox.Text = value.ToString();
			}
		}
	}

	/// <summary>
	/// Textbox control
	/// </summary>
	public CMSTextBox TextBox
	{
		get
		{
			return txtText;
		}
	}

	public override bool IsValid()
	{		
		var valueString = (string)Value;

		var maxLength = 450;
		// NodeAlias is over length 450
		if (valueString.Length + 1 > maxLength)
		{			
			this.ValidationError = "The max length for nodeAlias is 450, (449 with /). Please shorten the nodeAlias field.";
			return false;
		}
		var nodeIdString = this.Request.QueryString.Get("nodeid");
		var cultureString = this.Request.QueryString.Get("culture");
		if(!string.IsNullOrEmpty(nodeIdString) && !string.IsNullOrEmpty(cultureString))
		{
			if(int.TryParse(nodeIdString, out int nodeId))
			{
				var treeProvider = new TreeProvider(MembershipContext.AuthenticatedUser);
				var node = DocumentHelper.GetDocument(nodeId, cultureString, treeProvider);
				var nodeParentAliasPath = node.NodeAliasPath.Substring(0, node.NodeAliasPath.Length - (node.NodeAlias.Length + 1));
				if (nodeParentAliasPath == "/")
				{
					nodeParentAliasPath = "";
				}

				var length = valueString.Length + nodeParentAliasPath.Length + 1;
				if(length > maxLength)
				{					
					this.ValidationError = $"The max length for entire url is 450. The current slug can have a max length of {maxLength - (nodeParentAliasPath.Length + 1)}";
					return false;
				}
			}


		}


		return true;
	}
}
