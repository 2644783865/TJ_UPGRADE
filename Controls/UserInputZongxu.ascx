<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="UserInputZongxu.ascx.cs" Inherits="ZCZJ_DPF.UserInputZongxu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
<table style="border: #B9D3EE 2px solid;">
    <tr>
      <td align="right">数据源>待复制父序:</td>
      <td style="border-right:#B9D3EE 1px solid;">
          <asp:TextBox ID="txtBeforeFX" runat="server" Width="100px"  AutoPostBack="true" onfocus="this.select();"  OnTextChanged="txtBeforeFX_OnTextChanged"  onchange="this.title=this.value;"></asp:TextBox>
          <cc1:AutoCompleteExtender ID="AutoCompleteExtender"  runat="server" CompletionSetCount="15" CompletionListCssClass="autocomplete_completionListElement"  CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                DelimiterCharacters="" Enabled="True"  MinimumPrefixLength="1" ServiceMethod="GetSuggestions" FirstRowSelected="true" 
                ServicePath="~/TM_Data/TM_WebService.asmx" TargetControlID="txtBeforeFX" UseContextKey="True" CompletionInterval="10">
          </cc1:AutoCompleteExtender>
          <asp:ImageButton ID="imbgSource" runat="server"  OnClick="imbgSource_OnClick" ImageUrl="~/Assets/icons/forum.gif" ToolTip="查看《数据源》信息" />
          <asp:ImageButton ID="imbgNotImport" runat="server"  OnClick="imbgNotImport_OnClick" ImageUrl="~/Assets/icons/add.gif" ToolTip="不导入项设置" /><asp:Label ID="Label1" ForeColor="Red" runat="server" Text=""></asp:Label>
            <%--<cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Bottom"  Enabled="true" runat="server" OffsetX="-200"  OffsetY="0"  TargetControlID="imbgNotImport" PopupControlID="palORG">
            </cc1:PopupControlExtender>--%>
            <cc1:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="imbgNotImport"
                                            PopupControlID="palORG" Drag="false" Enabled="True" DynamicServicePath="" Y="80">
                                         </cc1:ModalPopupExtender>
                            <asp:Panel ID="palORG"  style="display:none;border-style:solid;border-width:1px;border-color:blue;background-color:Menu; width:250px;" runat="server">
                             <table width="100%" id="tbl">
                             <tr>       
                             </tr>
                             <tr>
                              <td>请勾选不导入的项</td>
                              <td align="center">
                                  <asp:Button ID="btnClose" runat="server" OnClick="btnClose_OnClick" Text="关闭" /></td>
                             </tr>
                             <tr><td>显示级数:
                              <asp:DropDownList ID="ddlOrgJishu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJishu_OnSelectedIndexChanged">
                                 <asp:ListItem Text="1级" Value="0"></asp:ListItem>
                                 <asp:ListItem Text="2级" Value="1" Selected="true"></asp:ListItem>
                                 <asp:ListItem Text="3级" Value="2"></asp:ListItem>
                                 <asp:ListItem Text="4级" Value="3"></asp:ListItem>
                                 <asp:ListItem Text="5级" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="6级" Value="5"></asp:ListItem>
                                 <asp:ListItem Text="7级" Value="6"></asp:ListItem>
                                 <asp:ListItem Text="8级" Value="7"></asp:ListItem>
                               </asp:DropDownList></td>
                             </tr>
                             <tr>
                              <td colspan="2">
                                <asp:TreeView ID="TreeViewMp" runat="server"  OnTreeNodeCheckChanged="TreeViewMp_OnTreeNodeCheckChanged"
                                    Font-Name= "Arial"
                                    RootNodeStyle-Font-Bold="true"
                                    SelectedNodeStyle-ForeColor="Red"
                                    ForeColor="Black"
                                    PopulateNodesFromClsient="true"
                                    ShowLines="true"
                                    NodeIndent = "20"
                                    ToolTip="总序-名称"
                                    onclick="NodeCheck();" >
                                </asp:TreeView>
                              </td>
                             </tr>
                             </table>
                             <br />
                            </asp:Panel>
          </td>
      <td align="right">目&nbsp;&nbsp;&nbsp;标>复制后父序:</td>    
       <td style="border-right:#B9D3EE 1px solid;">
           <asp:TextBox ID="txtAfterFX" runat="server"  Width="100px" AutoPostBack="true" onfocus="this.select();"  OnTextChanged="txtTarget_OnTextChanged" onblur='if(this.value!=""){ var pattem=/^([1-9]{1,2}(\(\d\))?(\.[1-9]{1}\d*(\(\d\))?)*)$/;if(!pattem.test(this.value)){alert("请输入正确的格式！！！\r\r正确格式为:1或1.m(.n),m、n为正整数");this.focus();this.value="";}}'></asp:TextBox>
           <asp:ImageButton ID="imgbTarget" runat="server" OnClick="imgbTarget_OnClick"  ImageUrl="~/Assets/icons/forum.gif" ToolTip="查看《目标》信息" /></td>   
       <td  align="center">
           <asp:Label ID="lblTip" Font-Bold="true" ForeColor="Red" Visible="false" runat="server" Text=""></asp:Label>
      </td>
      <td align="center">
            <asp:LinkButton ID="lkbDetail" runat="server" ForeColor="Red" OnClick="lkbDetail_OnClick">||导入信息||</asp:LinkButton>
           &nbsp;&nbsp;<asp:ImageButton ID="imgbDelete" runat="server" OnClick="imbDelete_OnClick" OnClientClick="return confirm('确认删除吗？')" ImageUrl="~/Assets/icons/delete.gif" />
      </td>
      </tr>
</table>
