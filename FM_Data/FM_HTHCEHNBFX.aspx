<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_HTHCEHNBFX.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_HTHCEHNBFX" Title="成本按合同号汇总" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
成本按合同号汇总
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
      <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>
      
      <script language="javascript" type="text/javascript">
           window.onload=function(){
                var tab = document.getElementById("tab");
                for (i = 1; i < (tab.rows.length-1); i++)
                {
                    var maolilv1=tab.rows[i].getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML;
                    var maolilv2=tab.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML;
                    var ml01=parseFloat(maolilv1).toFixed(2);
                    var ml02=parseFloat(maolilv2).toFixed(2);
                    tab.rows[i].getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML=ml01;
                    tab.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML=ml02;
                }
           }
      
           
           function redirectw(obj){
                htcode = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                window.open("CM_ProNum_ProfitLoss.aspx?htcode=" + htcode + "");
           }
      </script>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="20">
      </asp:ToolkitScriptManager>
      <div class="box_right">
        <div class="box-inner">
            <table id="tab0" style="width: 100%">
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;
                        业主名称：<asp:TextBox ID="txtyzname" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        合同号：<asp:TextBox ID="txthtcode" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp
                        业主合同号：<asp:TextBox ID="txtyzhtcode" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        项目名称：<asp:TextBox ID="txtengname" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        设备名称：<asp:TextBox ID="txtshebeiname" ForeColor="Gray" runat="server" Width="120px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCx" OnClick="btnCx_OnClick" runat="server" Text="查询"></asp:Button>
                   </td>
                   <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_plexport" Text="导出" OnClick="btn_plexport_OnClick" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="kaipiao" OnCheckedChanged="radio_kaipiaoif_CheckedChanged"
                                            AutoPostBack="True"  Checked="true" />
                        <asp:RadioButton ID="radio_yikaip" runat="server" Text="已开票" GroupName="kaipiao" OnCheckedChanged="radio_kaipiaoif_CheckedChanged"
                                            AutoPostBack="True"/>
                        <asp:RadioButton ID="radio_weikaip" runat="server" Text="未开票" GroupName="kaipiao" OnCheckedChanged="radio_kaipiaoif_CheckedChanged"
                                            AutoPostBack="True"/>
                    </td>
                    <td align="right">
                        <asp:CheckBox ID="canseeif" runat="server" AutoPostBack="true" OnCheckedChanged="displayif_onclick" />隐藏明细
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
      <div class="box_right">
                <div class="box-wrapper">
                    <div style="width: 100%; height: auto; overflow: scroll; display: block">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                            <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            业主名称
                                        </td>
                                        <td>
                                            合同号
                                        </td>
                                        <td>
                                            业主合同号
                                        </td>
                                        <td>
                                            项目名称
                                        </td>
                                        <td>
                                            设备名称
                                        </td>
                                        <td>
                                            销售合同金额
                                        </td>
                                        
                                        <td runat="server" id="tdzjrgf">
                                            直接人工费
                                        </td>
                                        <td runat="server" id="tdzjclf">
                                            直接材料费 
                                        </td>
                                        <td runat="server" id="tdzzfy">
                                            制造费用
                                        </td>
                                        <td runat="server" id="tdwxfy">
                                            外协费用
                                        </td>
                                        <td runat="server" id="tdcnfb">
                                            厂内分包
                                        </td>
                                        <td runat="server" id="tdyf">
                                            运费
                                        </td>
                                        <td runat="server" id="tdfjcb">
                                            分交成本
                                        </td>
                                        
                                        <td>
                                            成本(除制造费用外)
                                        </td>
                                        <td>
                                            总成本
                                        </td>
                                        
                                        <td>
                                            毛利率(不含制造费)
                                        </td>
                                        <td>
                                            毛利率(含制造费)
                                        </td>
                                        <td>
                                            开票金额
                                        </td>
                                        <td>
                                            开票状态
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr ondblclick="redirectw(this)">
                                        <td align="center">
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbyzname" runat="server" Text='<%#Eval("PCON_CUSTMNAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbhtcode" runat="server" Text='<%#Eval("PCON_BCODE")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbyzhtcode" runat="server" Text='<%#Eval("PCON_YZHTH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbengname" runat="server" Text='<%#Eval("PCON_ENGNAME")%>'></asp:Label>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:Label ID="lbsebeiname" runat="server" Text='<%#Eval("PCON_ENGTYPE")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbxshtje" runat="server" Text='<%#Eval("PCON_JINE")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td align="center" runat="server" id="tdzjrg">
                                            <asp:Label ID="lbzjrg" runat="server" Text='<%#Eval("RWHCB_ZJRG")%>'></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdcl">
                                            <asp:Label ID="lbcl" runat="server" Text='<%#Eval("RWHCB_CL")%>'></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdzzf">
                                            <asp:Label ID="lbzzfy" runat="server" Text='<%#Eval("RWHCB_ZZFY")%>'></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdwx">
                                            <asp:Label ID="lbwxfy" runat="server" Text='<%#Eval("RWHCB_WXFY")%>'></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdcnfb">
                                            <asp:Label ID="lbcnfb" runat="server" Text='<%#Eval("RWHCB_CNFB")%>'></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdyf">
                                            <asp:Label ID="lbyf" runat="server" Text='<%#Eval("RWHCB_YF")%>'></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdfjcb">
                                            <asp:Label ID="lbfjcb" runat="server" Text='<%#Eval("RWHCB_FJCB")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td align="center">
                                            <asp:Label ID="lbchengben" runat="server" Text='<%#Eval("RWHCB_CBZJ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbcbzj" runat="server" Text='<%#Eval("CBZJ")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td>
                                            <asp:Label ID="lbmaolilv1" runat="server" Text='<%#Eval("maolilv1")%>'></asp:Label>%
                                        </td>
                                        <td>
                                            <asp:Label ID="lbmaolilv2" runat="server" Text='<%#Eval("maolilv2")%>'></asp:Label>%
                                        </td>
                                        <td>
                                            <asp:Label ID="lbkpje" runat="server" Text='<%#Eval("kpZongMoney")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbkpstate" runat="server" Text='<%#Eval("kpstate")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="6" align="right">
                                            合计：
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_xshtzj" runat="server"></asp:Label>
                                        </td>
                                        
                                        <td align="center" runat="server" id="tdzjrgf">
                                            <asp:Label ID="lb_zjrgzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdclf">
                                            <asp:Label ID="lbclzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdzzfy">
                                            <asp:Label ID="lbzzfyzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdwxfy">
                                            <asp:Label ID="lbwxfyzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdcnfb">
                                            <asp:Label ID="lbcnfbzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdyf">
                                            <asp:Label ID="lbyfzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" runat="server" id="tdfjcb">
                                            <asp:Label ID="lbfjcbzj" runat="server"></asp:Label>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:Label ID="lb_cbzj" runat="server"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_cbtotal" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbkpjezj" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData" runat="server" ForeColor="Red" Visible="false" HorizontalAlign="Center">
                            <br />
                            没有记录<br />
                        </asp:Panel>
                    </div>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
</asp:Content>
