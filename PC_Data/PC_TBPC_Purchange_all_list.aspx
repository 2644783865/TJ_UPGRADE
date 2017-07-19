<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchange_all_list.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchange_all_list"
    Title="变更管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
 <link type="text/css" href="FixTable.css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
    function openmodewin()
    {
        var tablenmid_code;
        var autonum=Math.round(10000*Math.random());
        tablenmid_code=document.getElementById("<%=hid_filter.ClientID %>").value;
        window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum='+autonum+'&tablenmid_code='+tablenmid_code+'','',"dialogHeight:400px;dialogWidth:620px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
        window.location.href="PC_TBPC_Purchange_all_list.aspx";
    }
//     self.moveTo(0,0)
//    self.resizeTo(screen.availWidth,screen.availHeight)
</script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server"  Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        变更查询
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rad_quanbu" runat="server" Text="全部" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_quanbu_CheckedChanged" AutoPostBack="true" Checked="true"  Visible="false"/>&nbsp;
                                        <asp:RadioButton ID="rad_mytask" runat="server" Text="我的任务" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_mytask_CheckedChanged" AutoPostBack="true"  Visible="false"/>&nbsp; &nbsp;&nbsp;&nbsp;
                                        状态:
                                        <asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="0">未执行</asp:ListItem>
                                            <asp:ListItem Value="1">进行中</asp:ListItem>
                                            <asp:ListItem Value="2">完毕</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td>
                                    项目:
                                    <asp:TextBox ID="Tb_PJID" runat="server" Text="" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="Tb_PJNAME" runat="server" Text=""></asp:TextBox>
                                    设备:
                                    <asp:TextBox ID="Tb_ENGID" runat="server" Text="" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="Tb_ENGNAME" runat="server" Text=""></asp:TextBox>
                                    开始日期:<asp:TextBox ID="tb_riqif" runat="server" Text="" Width="100px"></asp:TextBox>
                                    结束日期:<asp:TextBox ID="tb_riqit" runat="server" Text="" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tb_riqif"
                                        Format="yyyy-MM-dd">
                                    </cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tb_riqit"
                                        Format="yyyy-MM-dd">
                                    </cc1:CalendarExtender>
                                    批号:<asp:TextBox ID="tb_danju" runat="server" Text="" Width="180px"></asp:TextBox>&nbsp;&nbsp;
                                    申请人:<asp:DropDownList ID="drp_st" runat="server">
                                    </asp:DropDownList>
                                    <asp:Button ID="btn_filter" runat="server" Text="过滤" OnClientClick="openmodewin()" Visible="false"/>
                                        <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>
                                    <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />
                                    <asp:Button ID="btn_clear" runat="server" Text="清除" OnClick="btn_clear_click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" align="center" class="nowrap cptable fullwidth" border="1">
                            <asp:Repeater ID="purch_list_Repeater" runat="server" OnItemDataBound="purch_list_Repeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color:#B9D3EE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>变更批号</strong>
                                        </td>
                                        <td>
                                            <strong>项目</strong>
                                        </td>
                                        <td>
                                            <strong>设备</strong>
                                        </td>
                                        <td>
                                            <strong>类型</strong>
                                        </td>
                                        <td>
                                            <strong>申请人</strong>
                                        </td>
                                        <td>
                                            <strong>日期</strong>
                                        </td>
                                        <td>
                                            <strong>负责人</strong>
                                        </td>
                                        <%-- <td>
                                            <strong>执行人</strong>
                                        </td>--%>
                                        <td>
                                            <strong>状态</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td runat="server" id="hlookup">
                                            <strong>查看</strong>
                                        </td>
                                        <td runat="server" id="hedit">
                                            <strong>修改</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*15%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHPCODE" runat="server" Text='<%#Eval("MP_CHPCODE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHPJID" runat="server" Text='<%#Eval("MP_CHPJID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CHPJNAME" runat="server" Text='<%#Eval("MP_CHPJNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHENGID" runat="server" Text='<%#Eval("MP_CHENGID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CHENGNAME" runat="server" Text='<%#Eval("MP_CHENGNAME")%>'></asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label ID="MP_MASHAPE" runat="server" Text='<%#Eval("MP_MASHAPE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHSUBMITNMID" runat="server" Text='<%#Eval("MP_CHSUBMITNMID")%>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CHSUBMITNM" runat="server" Text='<%#Eval("MP_CHSUBMITNM")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHSUBMITTM" runat="server" Text='<%#Eval("MP_CHSUBMITTM")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHREVIEWA" runat="server" Text='<%#Eval("MP_CHREVIEWA")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CHREVIEWANAME" runat="server" Text='<%#Eval("MP_CHREVIEWANAME")%>'></asp:Label>
                                        </td>
                                        <%--<td>
                                            <asp:Label ID="MP_CHREVIEWB" runat="server" Text='<%#Eval("MP_CHREVIEWB")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CHREVIEWBNAME" runat="server" Text='<%#Eval("MP_CHREVIEWBNAME")%>'></asp:Label>
                                        </td>--%>
                                        <td>
                                            <asp:Label ID="MP_CHSTATE" runat="server" Text='<%#get_ch_state(Eval("MP_CHSTATE").ToString())%>'></asp:Label>
                                            <asp:Label ID="MP_STATE" runat="server" Text='<%#Eval("MP_CHSTATE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHNOTE" runat="server" Text='<%#Eval("MP_CHNOTE")%>'></asp:Label>
                                        </td>
                                        <td runat="server" id="blookup">
                                            <asp:HyperLink ID="HyperLink_lookup" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Purchange_all.aspx?chpcd="+Eval("MP_CHPCODE")%>'>详细信息</asp:HyperLink>
                                        </td>
                                        <td runat="server" id="bedit">
                                            <asp:HyperLink ID="HyperLink_edit" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Purchange_all.aspx?chpcd="+Eval("MP_CHPCODE")%>'>详细信息</asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="11" align="center">
                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                        没有记录！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table  width="100%" border="1" class="nowrap cptable fullwidth" align="center">
                             <tr>
                             <td>
                               共<asp:Label ID="lb_count" runat="server" Text="Label"></asp:Label>条记录&nbsp;
                                共<asp:Label ID="lb_page" runat="server" Text="Label"></asp:Label>页 &nbsp; 
                                当前第<asp:Label ID="lb_CurrentPage" runat="server" Text="1"></asp:Label>页&nbsp;
                                <asp:LinkButton ID="LinkFirst" runat="server" OnClick="LinkFirst_Click">第一页</asp:LinkButton> &nbsp;
                                <asp:LinkButton ID="LinkUp" runat="server" OnClick="LinkUp_Click">上一页</asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="LinkDown" runat="server" OnClick="LinkDown_Click">下一页</asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="LinkLast" runat="server" OnClick="LinkLast_Click">最后一页</asp:LinkButton>&nbsp;
                                转到第<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>页
                              </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
