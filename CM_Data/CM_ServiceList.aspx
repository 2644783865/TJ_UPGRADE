<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_ServiceList.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务申请
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

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <table width="100%">
                <tr>
                    <td width="80px">
                        <asp:Button ID="SeleAll" runat="server" Text="显示全部" OnClick="SeleAll_Click" />
                    </td>
                    <td align="right" width="80px">
                        <asp:DropDownList ID="ddlBz" runat="server">
                            <asp:ListItem Value="CM_PJNAME">项目名称</asp:ListItem>
                            <asp:ListItem Value="CM_CONTR">合同号</asp:ListItem>
                            <asp:ListItem Value="CM_EQUIP">设备名称</asp:ListItem>
                            <asp:ListItem Value="CM_CUSNAME">顾客名称</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="middle" width="180px">
                        <asp:TextBox ID="txtBox" runat="server" onkeydown="return OnTxtPersonInfoKeyDown();"
                            Width="100px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="acName" runat="server" TargetControlID="txtBox" ServicePath="CM_Applica.asmx"
                            ServiceMethod="GetData" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="500"
                            EnableCaching="false" CompletionListCssClass="completionListElement" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="highlightedListItem">
                        </asp:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
                    </td>
                    <td id="Lb_Hint" runat="server" align="center" width="100px">
                        审核状态：
                    </td>
                    <td style="width: 150px" runat="server" id="tb_myTask">
                        <asp:RadioButtonList ID="rbl_mytask" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                            <asp:ListItem Text="我的任务" Value="1" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" runat="server" id="tb_status">
                        <asp:RadioButtonList ID="rbl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="待审核" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbl_depchuli" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                            RepeatDirection="Horizontal" Visible="false">
                            <asp:ListItem Text="未填写" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已填写" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" width="80px">
                        是否处理：
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rbl_chuli" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="未处理" Value="N"></asp:ListItem>
                            <asp:ListItem Text="已处理" Value="Y"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:Button ID="btChuLi" runat="server" Text="已处理" OnClick="btChuLi_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr runat="server" id="tb_add">
                    <td style="width: 350px">
                    </td>
                    <td align="center" width="100px">
                        联系单状态：
                    </td>
                    <td width="280px">
                        <asp:RadioButtonList ID="Rbl_LianXi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                            RepeatDirection="Horizontal" Width="260px">
                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                            <asp:ListItem Text="未下发" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已下发" Value="2"></asp:ListItem>
                            <asp:ListItem Text="被驳回" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:RadioButtonList ID="Rbl_LianXiState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                            RepeatDirection="Horizontal" Visible="false">
                            <asp:ListItem Text="待审核" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                            <asp:ListItem Text="被驳回" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="hpTask" NavigateUrl="CM_ServiceAdd.aspx?action=Add" runat="server">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            添加服务通知单</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <div class="box-wrapper" style="width: 100%">
                <div class="box-outer">
                    <div style="overflow: scroll">
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
                                            <strong>顾客名称</strong>
                                        </td>
                                        <td>
                                            <strong>合同号</strong>
                                        </td>
                                        <td>
                                            <strong>项目名称</strong>
                                        </td>
                                        <td>
                                            <strong>设备名称</strong>
                                        </td>
                                        <td>
                                            <strong>信息简介</strong>
                                        </td>
                                        <td>
                                            <strong>通知部门</strong>
                                        </td>
                                        <td>
                                            <strong>处理部门</strong>
                                        </td>
                                        <td>
                                            <strong>经办人</strong>
                                        </td>
                                        <td>
                                            <strong>制单时间</strong>
                                        </td>
                                        <td>
                                            <strong>审批状态</strong>
                                        </td>
                                        <td>
                                            <strong>查看</strong>
                                        </td>
                                        <td>
                                            <strong>处理表</strong>
                                        </td>
                                        <td>
                                            <strong>状态</strong>
                                        </td>
                                        <td>
                                            <strong>修改</strong>
                                        </td>
                                        <td runat="server">
                                            <strong>审批</strong>
                                        </td>
                                        <td>
                                            <strong>联系单下发</strong>
                                        </td>
                                        <td>
                                            <strong>联系单状态</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
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
                                            <%#Eval("CM_CUSNAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_CONTR")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_PJNAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_EQUIP")%>
                                        </td>
                                        <td>
                                            <p id="pnote" title='<%#Eval("CM_INFO").ToString()==""?"无信息简介":Eval("CM_INFO")%>'>
                                                备注...<p>
                                        </td>
                                        <td>
                                            <%#Eval("CM_PART")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_CLPART")%>
                                        </td>
                                        <td>
                                            <%#Eval("ST_NAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_ZDTIME")%>
                                        </td>
                                        <td>
                                            <%#Eval("CMSTATUS")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink1" runat="server" CssClass="link" NavigateUrl='<%#"CM_ServiceAdd.aspx?action=Look&id="+Eval("CM_ID") %>'
                                                Target="_blank">
                                                <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                查看
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink4" runat="server" CssClass="link" NavigateUrl='<%#"CM_ServiceSolve.aspx?id="+Eval("CM_ID") %>'
                                                Target="_blank">
                                                <asp:Image ID="image4" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                处理表
                                            </asp:HyperLink>
                                        </td>
                                        <td runat="server" id="tdZT">
                                            <asp:Label runat="server" ID="lbZT" Text='<%#Eval("CM_CHULI")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink2" runat="server" CssClass="link" Visible="false" NavigateUrl='<%#"CM_ServiceAdd.aspx?action=Edit&id="+Eval("CM_ID") %>'
                                                Target="_blank">
                                                <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                修改
                                            </asp:HyperLink>
                                        </td>
                                        <td runat="server">
                                            <asp:HyperLink ID="hyperlink3" runat="server" CssClass="link" Visible="false" NavigateUrl='<%#"CM_ServiceAdd.aspx?action=ShenP&id="+Eval("CM_ID") %>'
                                                Target="_blank">
                                                <asp:Image ID="image3" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                审批
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink5" runat="server" CssClass="link" NavigateUrl='<%#"CM_ServiceConn.aspx?id="+Eval("CM_ID") %>'
                                                Target="_blank">
                                                <asp:Image ID="image5" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                联系单
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <%#Eval("CM_STATE").ToString() == "1" ? "未下发":Eval("CM_STATE").ToString()=="2"?"已下发":"被驳回"%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <br />
                    <asp:Button ID="btn_del" runat="server" Text="删 除" OnClick="btn_del_Click" OnClientClick="return confirm('你确定删除吗?');" />
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
