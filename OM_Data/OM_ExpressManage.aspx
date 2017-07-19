<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_ExpressManage.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ExpressManage"
    Title="快递管理" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    快递管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <table width="100%">
        <tr>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <td>
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="Query"
                    RepeatColumns="7" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                    <asp:ListItem Text="待审核" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                    <asp:ListItem Text="已反馈" Value="4"></asp:ListItem>
                    <asp:ListItem Text="我的任务" Value="5" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="right">
                <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_ExpressDetail.aspx?action=add" runat="server">
                    <asp:Image ID="ImgAdd" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    快递申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>条件选择 </strong>&nbsp;&nbsp;&nbsp;快递公司：
                <asp:DropDownList runat="server" ID="ddlE_Company" OnSelectedIndexChanged="Query"
                    AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="百世汇通" Value="0"></asp:ListItem>
                    <asp:ListItem Text="顺丰" Value="1"></asp:ListItem>
                    <asp:ListItem Text="邮政EMS" Value="2"></asp:ListItem>
                    <asp:ListItem Text="物流" Value="3"></asp:ListItem>
                    <asp:ListItem Text="其他" Value="4"></asp:ListItem>
                    <asp:ListItem Text="自行联系" Value="5"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;邮寄类别：<asp:DropDownList ID="ddlE_Type" runat="server" OnSelectedIndexChanged="Query"
                    AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="文件类" Value="0"></asp:ListItem>
                    <asp:ListItem Text="物品类" Value="1"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp; 申请人：
                <asp:TextBox runat="server" ID="txtE_SQR" Width="60px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp; 快递单号：
                <asp:TextBox runat="server" ID="txtE_ExpressCode" Width="100px"></asp:TextBox>&nbsp;&nbsp;制单时间：
                <asp:TextBox runat="server" ID="txtE_ZDTimeS" class="easyui-datebox" 
                    Width="100px"></asp:TextBox>-
                <asp:TextBox runat="server" ID="txtE_ZDTimeE" class="easyui-datebox" 
                    Width="100px"> </asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="Query" />
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer" style="overflow: auto">
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <asp:Repeater ID="rptExpress" runat="server" OnItemDataBound="rptExpress_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor" height="30px" runat="server">
                            <td>
                                <strong>序号 </strong>
                            </td>
                            <td>
                                <strong>申请单号 </strong>
                            </td>
                            <td>
                                <strong>申请人</strong>
                            </td>
                            <td>
                                <strong>邮寄类型</strong>
                            </td>
                            <td>
                                <strong>邮寄内容/用途</strong>
                            </td>
                            <td>
                                <strong>收件单位</strong>
                            </td>
                            <td>
                                <strong>邮寄地址</strong>
                            </td>
                            <td>
                                <strong>特殊说明</strong>
                            </td>
                            <td>
                                <strong>状态</strong>
                            </td>
                            <td>
                                <strong>快递公司</strong>
                            </td>
                            <td>
                                <strong>快递单号</strong>
                            </td>
                            <td>
                                <strong>快递费用</strong>
                            </td>
                            <td>
                                <strong>备注(反馈)</strong>
                            </td>
                            <td>
                                <strong>查看</strong>
                            </td>
                            <td id="edit" runat="server">
                                <strong>编辑</strong>
                            </td>
                            <td id="audit" runat="server">
                                <strong>审核</strong>
                            </td>
                            <td id="sure" runat="server">
                                <strong>反馈</strong>
                            </td>
                            <td id="delete" runat="server">
                                <strong>删除</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                            runat="server">
                            <td>
                                <asp:Label ID="lbXuhao" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbE_Code" Text='<%#Eval("E_Code")%>' runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbE_SQR" runat="server" Text=' <%#Eval("E_SQR")%>' ToolTip='<%#Eval("E_SQRDep")%>'></asp:Label>
                            </td>
                            <td>
                                <%# Eval("E_Type").ToString().Contains("0") ? "文件类" : "物品类"%>
                            </td>
                            <td>
                                <%#Eval("E_Type").ToString() == "0" ? Eval("E_FileName").ToString() : Eval("E_ItemName").ToString() +','+ Eval("E_ItemWeight").ToString()%>
                            </td>
                            <td>
                                <%#Eval("E_SendTo")%>
                            </td>
                            <td>
                                <%#Eval("E_Address")%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtE_Note" TextMode="MultiLine" runat="server" Width="100px" Text='<%#Eval("E_Note")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbE_State" runat="server" Text='<%#Eval("E_State").ToString()=="0"?"未提交":Eval("E_State").ToString()=="1"?"待审核":Eval("E_State").ToString()=="2"?"已通过":Eval("E_State").ToString()=="3"?"已驳回":Eval("E_State").ToString()=="5"?"反馈驳回":"已反馈"%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbE_ExpressCompany" runat="server" Text='<%#Eval("E_ExpressCompany").ToString()=="0"?"百世汇通":Eval("E_ExpressCompany").ToString()=="1"?"顺丰":Eval("E_ExpressCompany").ToString()=="2"?"邮政EMS":Eval("E_ExpressCompany").ToString()=="3"?"物流":Eval("E_ExpressCompany").ToString()=="4"?"其他":Eval("E_ExpressCompany").ToString()=="5"?"自行联系":""%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("E_ExpressCode")%>
                            </td>
                            <td>
                                <%#Eval("E_ExpressMoney")%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtE_BackNote" TextMode="MultiLine" Width="100px" runat="server"
                                    Text='<%#Eval("E_BackNote")%>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ExpressDetail.aspx?action=view&key="+Eval("E_Code")%>'
                                    runat="server" Target="_blank" ID="hpView">
                                    <asp:Image ID="ImgView" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />查看
                                </asp:HyperLink>
                            </td>
                            <td width="100px" id="tdedit" runat="server">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ExpressDetail.aspx?action=edit&key="+Eval("E_Code")%>'
                                    runat="server" Target="_blank" ID="hpEdit">
                                    <asp:Image ID="ImgEdit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />编辑
                                </asp:HyperLink>
                            </td>
                            <td width="100px" id="tdaudit" runat="server">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ExpressDetail.aspx?action=audit&key="+Eval("E_Code")%>'
                                    runat="server" Target="_blank" ID="hpAudit">
                                    <asp:Image ID="ImgAudit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />审核
                                </asp:HyperLink>
                            </td>
                            <td width="100px" id="tdsure" runat="server">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ExpressDetail.aspx?action=sure&key="+Eval("E_Code")%>'
                                    runat="server" Target="_blank" ID="hpSure">
                                    <asp:Image ID="ImgSure" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />反馈</asp:HyperLink>
                            </td>
                            <td width="100px" id="tddelete" runat="server">
                                <asp:LinkButton runat="server" ID="lkbDelete" OnClick="lkbDelete_OnClick" CommandName='<%#Eval("E_Code") %>'
                                    OnClientClick="return confirm(&quot;确认删除吗？&quot;)">
                                    <asp:Image ID="ImgDelete" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="text-align: center; padding-top: 6px" id="divHJ" runat="server">
                <asp:Label ID="lbCount" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>&nbsp;条记录，快递费用共计&nbsp;
                <asp:Label ID="lbTotalMoney" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>&nbsp;元
            </div>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                没有记录!</asp:Panel>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
