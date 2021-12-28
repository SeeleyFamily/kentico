<%@ Control Language="C#" AutoEventWireup="true" Inherits="Launchpad_FormControls_Documents_SelectDocuments"
     Codebehind="SelectDocuments.ascx.cs" %>
<cms:CMSUpdatePanel ID="pnlUpdateHidden" runat="server">
    <ContentTemplate>
        <div class="control-group-inline">
            <cms:CMSTextBox ID="txtName" runat="server" MaxLength="800" CssClass="form-control" />
            <cms:CMSButton
                ID="btnSelect" runat="server" ButtonStyle="Default" />
            <cms:CMSButton ID="btnClear" runat="server" ButtonStyle="Default" />
            <cms:CMSButton ID="btnAdd" runat="server" ButtonStyle="Default" />
            <cms:CMSTextBox ID="txtGuidTemp" runat="server" AutoPostBack="true" CssClass="Hidden_" />
            <cms:CMSTextBox ID="txtGuid" runat="server" CssClass="Hidden_" />
            <cms:LocalizedLabel ID="lblGuid" runat="server" EnableViewState="false" Display="false" AssociatedControlID="txtGuid" ResourceString="development_formusercontrol_edit.lblforguid" />
        </div>
        <div style="margin-top:10px;">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th class="col">
                            Move
                        </th>
                        <th class="filling-column">
                            Page Name
                        </th>
                        <th class="col">
                            Delete
                        </th>
                        <th class="" scope="col">&nbsp;</th>
                    </tr>
                </thead>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Button CssClass="btn btn-secondary btn-mini" runat="server" CommandName="Up" CommandArgument='<%# Eval("Key") %>' Text="↑" />
                                <asp:Button CssClass="btn btn-secondary btn-mini" runat="server" CommandName="Down" CommandArgument='<%# Eval("Key") %>' Text="↓" />
                            </td>
                            <td class="filling-column">
                                <div style="display:inline-block;"><%# Eval("Value") %>
                            </td>
                            <td class="unigrid-actions">
                                <asp:Button runat="server" CommandName="Delete" CommandArgument='<%# Eval("Key") %>' Text="Delete" />
                            </td>
                            <td class="wrap-normal"></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
<style>
    .cms-bootstrap .btn.btn-mini {
        padding: 0px 8px;
        margin-left: 0px;
        height: 22px;
        line-height: 22px;
    }
</style>