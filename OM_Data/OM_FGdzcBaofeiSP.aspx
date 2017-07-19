<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_FGdzcBaofeiSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_FGdzcBaofeiSP"
    Title="非固定资产报废审批" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    非固定资产报废审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:RadioButton ID="radAll" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="Query"
                            AutoPostBack="True" />
                        <asp:RadioButton ID="radMytask" runat="server" Text="我的任务" GroupName="shenhe" OnCheckedChanged="Query"
                            AutoPostBack="True" Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 审批状态：<asp:DropDownList ID="drpState" runat="server"
                            OnSelectedIndexChanged="Query" AutoPostBack="True">
                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                            <asp:ListItem Value="0">初始化</asp:ListItem>
                            <asp:ListItem Value="1">待审批</asp:ListItem>
                            <asp:ListItem Value="2">审批中</asp:ListItem>
                            <asp:ListItem Value="3">已通过</asp:ListItem>
                            <asp:ListItem Value="4">已驳回</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        审批编号：<asp:TextBox runat="server" ID="txtSPBH" Width="80px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnChakan" Text="查询" OnClick="Query" BackColor="#98FB98" />
                        <asp:Button ID="btnReset" runat="server" Text="重置" BackColor="#98FB98" OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptFGDZCBF" runat="server" OnItemDataBound="rptFGDZCBF_OnItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #B9D3EE; height: 30px">
                                <td align="center">
                                    序号
                                </td>
                                <td align="center">
                                    审批编号
                                </td>
                                <td align="center">
                                    报废资产数
                                </td>
                                <td align="center">
                                    制单人
                                </td>
                                <td align="center">
                                    制单时间
                                </td>
                                <td align="center">
                                    审核状态
                                </td>
                                <td align="center">
                                    查看
                                </td>
                                <td align="center">
                                    修改
                                </td>
                                <td align="center">
                                    审核
                                </td>
                                <td align="center">
                                    删除
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                onclick="javascript:change(this);" ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:Label ID="lbXuHao" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbSPBH" runat="server" Text='<%#Eval("SPBH")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbSC_Num" runat="server" Width="50px" Text='<%#Eval("SC_Num")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbZDR_Name" runat="server" Width="50px" Text='<%#Eval("ZDR_Name")%>'></asp:Label>
                                    <asp:Label ID="lbSHR1_ID" runat="server" Width="50px" Visible="false" Text='<%#Eval("SHR1_ID")%>'></asp:Label>
                                    <asp:Label ID="lbSHR2_ID" runat="server" Width="50px" Visible="false" Text='<%#Eval("SHR2_ID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbZD_Time" runat="server" Width="90px" Text='<%#Eval("ZD_Time")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbSPZT" runat="server" Text='<%#Eval("SPZT").ToString()=="1"?"待审批":Eval("SPZT").ToString()=="3"?"已通过":Eval("SPZT").ToString()=="4"?"已驳回":Eval("SPZT").ToString()=="0"?"初始化":"审批中"%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink runat="server" ID="hlChaKan" Target="_blank" NavigateUrl='<%#"OM_FGdzcBaofei_Detail.aspx?action=read&id="+DataBinder.Eval(Container.DataItem,"SPBH").ToString()%>'>
                                        <asp:Image runat="server" ID="ImageChaKan" ImageUrl="~/Assets/images/read.gif" Height="20px"
                                            ImageAlign="AbsMiddle" />
                                        查看
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink runat="server" ID="hlXiuGai" NavigateUrl='<%#"OM_FGdzcBaofei_Detail.aspx?action=alter&id="+DataBinder.Eval(Container.DataItem,"SPBH").ToString()%>'>
                                        <asp:Image runat="server" ID="imgXiuGai" ImageUrl="~/Assets/images/res.gif" Height="20px"
                                            ImageAlign="AbsMiddle" />
                                        修改
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlContract" runat="server" NavigateUrl='<%#"OM_FGdzcBaofei_Detail.aspx?action=check&id="+DataBinder.Eval(Container.DataItem,"SPBH").ToString()%>'>
                                        <asp:Image ID="imgShenHe" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                        审核
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("SPBH")%>'
                                        CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/erase.gif" runat="server" border="0"
                                            hspace="2" align="absmiddle" />删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
