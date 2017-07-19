<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_ProductNumber_Statistics.aspx.cs"
    MasterPageFile="~/Masters/BaseMaster.master" Inherits="ZCZJ_DPF.FM_Data.FM_ProductNumber_Statistics" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    生产制号统计
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript" language="javascript">
function show(id)
{
  var starttime="<%=statetimes()%>";  
  var endTime="<%=endtimes()%>";  
  var state="<%=states()%>";    var sRet=window.showModalDialog("SCZHLL_PJ.aspx?id="+id+"&starttime="+starttime+"&endtime="+endTime+"&state="+state,"obj","dialogWidth=1200px;dialogHeight=600px;");
  if(sRet=="refresh")
  { 
    window.location.reload();
  }
}

    </script>

   <%-- <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <table width="100%">
        <tr>
            <td style="width: 30%">
                按时间查询：<asp:DropDownList ID="ddlchaxun" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlchaxun_SelectedIndexChanged">
                    <asp:ListItem Text="-未选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="本期" Value="1"></asp:ListItem>
                    <asp:ListItem Text="本周" Value="2"></asp:ListItem>
                    <asp:ListItem Text="当天" Value="3"></asp:ListItem>
                    <asp:ListItem Text="-自定义-" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left">
                <asp:Panel ID="palTime" runat="server" Visible="false">
                    <span>从&nbsp;</span><asp:TextBox ID="txtStartTime" onClick="setday(this)" runat="server"></asp:TextBox>
                    <span>到&nbsp;</span><asp:TextBox ID="txtEndTime" onClick="setday(this)" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClick="btnQuery_Click" />
                </asp:Panel>
            </td>
            <td>
                核算状态：<asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    <asp:ListItem Text="全部" Value="%"></asp:ListItem>
                    <asp:ListItem Text="未核算" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已核算" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Button ID="btn_export" runat="server" Text="导出Excel" OnClick="btn_export_Click" />
            </td>
        </tr>
    </table>
    <div id="div_statistcs" runat="server" style="width: 100%; overflow: scroll; overflow-y: auto;
        overflow-x: atuo; display: block;">
        <asp:Repeater ID="rptProductNumStc" runat="server" OnItemDataBound="rptProductNumStc_ItemDataBound">
            <HeaderTemplate>
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <tr align="center" class="tableTitle">
                        <td rowspan="2" align="left">
                            <strong>序号</strong>
                        </td>
                        <td rowspan="2" align="left">
                            <strong>生产制号</strong>
                        </td>
                        <td rowspan="2" align="left">
                            <strong>项目名称</strong>
                        </td>
                        <td rowspan="2" align="left">
                            <strong>工程名称</strong>
                        </td>
                        <td colspan="2" align="right">
                            <strong>黑色金属</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>标准件(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>厂内配件(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>电料(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>管件阀门(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>焊材类(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>化工橡胶(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>加工件(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>木材矿窑(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>燃油类(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>外购件(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>五金材料(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>消防器材(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>有色金属(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>油漆涂料(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>杂品(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>周转材料(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>电气电料(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>低值易耗(金额)</strong>
                        </td>
                        <td rowspan="2" align="right">
                            <strong>合计(金额)</strong>
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="right">
                            <strong>实发数量</strong>
                        </td>
                        <td align="right">
                            <strong>金额</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center" class="baseGadget" onclick="this.className='clickback'" onmouseout="this.className='baseGadget'"
                    ondblclick='javascript:show("<%#Eval("PS_SCZH") %>")'>
                    <td align="left">
                        <asp:Label ID="label8" runat="server" Text='<%#Container.ItemIndex + 1%>'></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="label1" runat="server" Text='<%#Eval("PS_SCZH")%>'></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="label4" runat="server" Text='<%#Eval("PJ_NAME")%>'></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="label6" runat="server" Text='<%#Eval("TSA_ENGNAME")%>'></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="label2" runat="server" Text='<%#Eval("PS_HSJSSFSL")%>'></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="label3" runat="server" Text='<%#Eval("PS_HSJSJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label4" runat="server" Text='<%#Eval("PS_BZJSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label5" runat="server" Text='<%#Eval("PS_BZJJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label6" runat="server" Text='<%#Eval("PS_CNPJSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label7" runat="server" Text='<%#Eval("PS_CNPJJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label8" runat="server" Text='<%#Eval("PS_DLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label9" runat="server" Text='<%#Eval("PS_DLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label10" runat="server" Text='<%#Eval("PS_GJFMSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label11" runat="server" Text='<%#Eval("PS_GJFMJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label12" runat="server" Text='<%#Eval("PS_HCLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label13" runat="server" Text='<%#Eval("PS_HCLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label14" runat="server" Text='<%#Eval("PS_HGXJSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label15" runat="server" Text='<%#Eval("PS_HGXJJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label16" runat="server" Text='<%#Eval("PS_JGJSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label17" runat="server" Text='<%#Eval("PS_JGJJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label18" runat="server" Text='<%#Eval("PS_MCKYSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label19" runat="server" Text='<%#Eval("PS_MCKYJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label20" runat="server" Text='<%#Eval("PS_RYLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label21" runat="server" Text='<%#Eval("PS_RYLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label22" runat="server" Text='<%#Eval("PS_WGJSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label23" runat="server" Text='<%#Eval("PS_WGJJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label24" runat="server" Text='<%#Eval("PS_WJCLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label25" runat="server" Text='<%#Eval("PS_WJCLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label26" runat="server" Text='<%#Eval("PS_XFQCSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label27" runat="server" Text='<%#Eval("PS_XFQCJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label28" runat="server" Text='<%#Eval("PS_YSJSSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label29" runat="server" Text='<%#Eval("PS_YSJSJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label30" runat="server" Text='<%#Eval("PS_YQTLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label31" runat="server" Text='<%#Eval("PS_YQTLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label32" runat="server" Text='<%#Eval("PS_ZPSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label33" runat="server" Text='<%#Eval("PS_ZPJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label34" runat="server" Text='<%#Eval("PS_ZZCLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label35" runat="server" Text='<%#Eval("PS_ZZCLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label36" runat="server" Text='<%#Eval("PS_DQDLSFSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label37" runat="server" Text='<%#Eval("PS_DQDLJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label38" runat="server" Text='<%#Eval("PS_GJSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label39" runat="server" Text='<%#Eval("PS_GJJE")%>'></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label40" runat="server" Text='<%#Eval("PS_HJSL")%>'></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label41" runat="server" Text='<%#Eval("PS_HJJE")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" Visible="false">LinkButton</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr align="right">
                    <td align="right" colspan="4">
                        汇总
                    </td>
                    <td align="right">
                        <asp:Label ID="label52" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="label53" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label54" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label55" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label56" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label57" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label58" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label59" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label60" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label61" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label62" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label63" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label64" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label65" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label66" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label67" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label68" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label69" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label70" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label71" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label72" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label73" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label74" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label75" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label76" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label77" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label78" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label79" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label80" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label81" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label82" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label83" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label84" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label85" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label86" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label87" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label88" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label89" runat="server"></asp:Label>
                    </td>
                    <%--<td><asp:Label ID="label90" runat="server"></asp:Label></td>--%>
                    <td align="right">
                        <asp:Label ID="label91" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
    </div>
    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
        <hr style="width: 100%; height: 0.1px; color: Blue;" />
        没有记录!</asp:Panel>
</asp:Content>
