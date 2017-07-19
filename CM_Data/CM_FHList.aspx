<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_FHList.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_FHList" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    发货通知
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
    
        function aa() {
            $("#tab tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        }
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
                    <td style="width: 150px">
                        <asp:RadioButtonList ID="rbl_mytask" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                            <asp:ListItem Text="我的任务" Value="1" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rbl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_mytask_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="待签字" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                            <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" width="100px">
                        搜索条件：
                    </td>
                    <td align="right">
                        <asp:DropDownList ID="ddlBz" runat="server">
                            <asp:ListItem Value="CM_CUSNAME">顾客名称</asp:ListItem>
                            <asp:ListItem Value="CM_PROJ">项目名称</asp:ListItem>
                            <asp:ListItem Value="CM_CONTR">合同号</asp:ListItem>
                            <asp:ListItem Value="TSA_ENGNAME">交货内容</asp:ListItem>
                            <asp:ListItem Value="TSA_MAP">图号</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="height: 42px" align="left" valign="middle" width="400px">
                        <asp:TextBox ID="txtBox" runat="server" onkeydown="return OnTxtPersonInfoKeyDown();"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="acName" runat="server" TargetControlID="txtBox" ServicePath="CM_Customer.asmx"
                            ServiceMethod="GetNotice" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="500"
                            EnableCaching="false" CompletionListCssClass="completionListElement" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="highlightedListItem">
                        </asp:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" Text="搜 索" OnClick="btnSearch_Click" />
                    </td>
                    <td align="right">
                        <asp:HyperLink ID="hpTask" NavigateUrl="CM_FHNotice.aspx?action=add" runat="server">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            添加发货通知</asp:HyperLink>
                    </td>
                </tr>
            </table>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div style="overflow: scroll;">
                        <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
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
                                            <strong>项目名称</strong>
                                        </td>
                                        <td>
                                            <strong>合同号</strong>
                                        </td>
                                        <td>
                                            <strong>交货内容</strong>
                                        </td>
                                        <td>
                                            <strong>图号</strong>
                                        </td>
                                        <td>
                                            <strong>收货单位</strong>
                                        </td>
                                        <td>
                                            <strong>交（提）货地点</strong>
                                        </td>
                                        <td>
                                            <strong>联系人</strong>
                                        </td>
                                        <td>
                                            <strong>联系方式</strong>
                                        </td>
                                        <td>
                                            <strong>要求发货时间</strong>
                                        </td>
                                        <td>
                                            <strong>制单时间</strong>
                                        </td>
                                        <td>
                                            <strong>经办人</strong>
                                        </td>
                                        <td>
                                            <strong>是否通过</strong>
                                        </td>
                                        <td>
                                            <strong>打印</strong>
                                        </td>
                                        <td>
                                            <strong>查看</strong>
                                        </td>
                                        <td>
                                            <strong>修改</strong>
                                        </td>
                                        <td>
                                            <strong>签字</strong>
                                        </td>
                                        <td>
                                            <strong>返审</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget">
                                        <td>
                                            <asp:Label ID="CM_FID" runat="server" Visible="false" Text='<%#Eval("CM_FID")%>'></asp:Label>
                                            <asp:Label ID="lblIndex" runat="server" Text='<%#Eval("ID_Num") %>'></asp:Label>
                                            <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server" />
                                        </td>
                                        <td>
                                            <%# Eval("CM_BIANHAO")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_CUSNAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_PROJ")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_CONTR")%>
                                        </td>
                                        <td>
                                            <%#Eval("TSA_ENGNAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("TSA_MAP")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_SH")%>
                                        </td>
                                        <td>
                                            <div style="width: 240px; white-space: normal">
                                                <%#Eval("CM_JH")%>
                                            </div>
                                        </td>
                                        <td>
                                            <%#Eval("CM_LXR")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_LXFS")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_JHTIME")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_ZDTIME")%>
                                        </td>
                                        <td>
                                            <%#Eval("MANCLERK")%>
                                        </td>
                                        <td>
                                            <%#Eval("CM_CONFIRM").ToString()=="1"?"评审中":Eval("CM_CONFIRM").ToString()=="2"?"通过":Eval("CM_CONFIRM").ToString()=="3"?"驳回":""%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplDY" runat="server" NavigateUrl='<%#"CM_FHTZDY.aspx?CM_FID="+Eval("CM_FID")%>'
                                                Target="_blank" CssClass="link">
                                                <asp:Image ID="image4" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/DaYin.jpg" />打印
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink2" runat="server" CssClass="link" NavigateUrl='<%#"CM_FHNotice.aspx?action=look&id="+Eval("CM_FID") %>'
                                                Target="_blank">
                                                <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                查看
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink1" runat="server" CssClass="link" NavigateUrl='<%#"CM_FHNotice.aspx?action=edit&id="+Eval("CM_FID")+"&confirm="+Eval("CM_CONFIRM") %>'
                                                Target="_blank" Visible="false">
                                                <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                修改
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hyperlink3" runat="server" CssClass="link" NavigateUrl='<%#"CM_FHNotice.aspx?action=sure&id="+Eval("CM_FID") %>'
                                                Target="_blank" Visible="false">
                                                <asp:Image ID="image3" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                                签字
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="lbtnBackCheck" Text="返审" CommandArgument='<%#Eval("CM_FID")%>'
                                                OnClientClick="return confirm('确认返审该合同？')" OnClick="lbtnBackCheck_OnClick">
                                                <asp:Image runat="server" ID="imgBackCheck" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                                    ImageUrl="~/Assets/images/erase.gif" />
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            没有记录!</asp:Panel>
                        <br />
                    </div>
                    <asp:Button ID="btn_del" runat="server" Text="删 除" OnClientClick="return confirm('你确定删除吗?');"
                        OnClick="btn_del_Click" />
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
