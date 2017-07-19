<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_ServiceFile.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceFile"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务文件
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .completionListElement
        {
            margin: 0px;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 150px !important;
            background-color: White;
            font-size: small;
        }
        .listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            color: windowtext;
            font-size: small;
        }
        .highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            font-size: small;
        }
    </style>

    <script type="text/javascript">
        function OnTxtPersonInfoKeyDown() {
            var dep = document.getElementById('<%=ddlBz.ClientID%>');
            var acNameClientId = "<%=acName.ClientID %>";
            var acName = $find(acNameClientId);
            if (acName != null) {
                acName.set_contextKey(dep.options[dep.selectedIndex].value);
            }
        }
    </script>

    <script type="text/javascript">
        function Read(a) {
            window.open("CM_Service.aspx?action=look&id=" + a);
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <table width="100%">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <tr>
            <td align="right" width="100px">
                搜索条件：
            </td>
            <td align="right" width="200px">
                <asp:DropDownList ID="ddlBz" runat="server">
                    <asp:ListItem Value="CM_CONTR">合同号</asp:ListItem>
                    <asp:ListItem Value="CM_EQUIP">设备名称</asp:ListItem>
                    <asp:ListItem Value="CM_CUSNAME">顾客名称</asp:ListItem>
                    <asp:ListItem Value="CM_EQUIPMAP">图号</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="height: 42px" valign="middle" align="left">
                <asp:TextBox ID="txtBox" runat="server" onkeydown="return OnTxtPersonInfoKeyDown();"></asp:TextBox>
                <asp:AutoCompleteExtender ID="acName" runat="server" TargetControlID="txtBox" ServicePath="CM_Customer.asmx"
                    ServiceMethod="GetFile" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="500"
                    EnableCaching="false" CompletionListCssClass="completionListElement" CompletionListItemCssClass="listItem"
                    CompletionListHighlightedItemCssClass="highlightedListItem">
                </asp:AutoCompleteExtender>
                <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
            </td>
            <td width="15%" align="right">
                <asp:HyperLink ID="hpTask" NavigateUrl="CM_Service.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    添加顾客服务文件</asp:HyperLink>
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1" style="cursor: pointer">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td width="50px">
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>编号</strong>
                                    </td>
                                    <td>
                                        <strong>合同号</strong>
                                    </td>
                                    <td>
                                        <strong>顾客名称</strong>
                                    </td>
                                    <td>
                                        <strong>设备名称</strong>
                                    </td>
                                    <td>
                                        <strong>设备图号</strong>
                                    </td>
                                    <td>
                                        <strong>服务开始时间</strong>
                                    </td>
                                    <td>
                                        <strong>服务结束时间</strong>
                                    </td>
                                    <td>
                                        <strong>服务内容概况</strong>
                                    </td>
                                    <td>
                                        <strong>服务结果</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                    <td>
                                        <strong>维护人</strong>
                                    </td>
                                    <td>
                                        <strong>查看</strong>
                                    </td>
                                    <td>
                                        <strong>修改</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                    ondblclick='Read(<%#Eval("CM_ID")%>)'>
                                    <asp:Label ID="CM_ID" runat="server" Visible="false" Text='<%#Eval("CM_ID")%>'></asp:Label>
                                    <asp:Label ID="CM_CONTEXT" runat="server" Visible="false" Text='<%#Eval("CM_CONTEXT")%>'></asp:Label>
                                    <td>
                                        <asp:Label ID="lblIndex" runat="server" Text='<%#Eval("ID_Num") %>'></asp:Label>
                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                    </td>
                                    <td>
                                        <%#Eval("CM_BIANHAO")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_CONTR")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_CUSNAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_EQUIP")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_EQUIPMAP")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_STARTTIME")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_ENDTIME")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_SERBASIC")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_RESULT")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_NOTE")%>
                                    </td>
                                    <td>
                                        <%#Eval("ST_NAME")%>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hyperlink2" runat="server" CssClass="link" NavigateUrl='<%#"CM_Service.aspx?action=look&id="+Eval("CM_ID") %>'
                                            Target="_blank">
                                            <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            查看
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hyperlink1" runat="server" CssClass="link" NavigateUrl='<%#"CM_Service.aspx?action=edit&id="+Eval("CM_ID") %>'
                                            Target="_blank" Visible="false">
                                            <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            修改
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <br />
                    <asp:Button ID="btn_del" runat="server" Text="删 除" OnClientClick="return confirm('你确定删除吗?');"
                        OnClick="btn_del_Click" />
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
