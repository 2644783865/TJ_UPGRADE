<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_PurOrder.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_PurOrder"
    Title="订单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购订单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">

        function pur_bjdshangcha(temp) {
            var i = window.showModalDialog("TBPC_IQRCMPPRCLST_checked.aspx?ptc=" + temp, '', "dialogHeight:400px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        function print() {
            var sheetno = "<%=gloabsheetno%>";
            top.location.href = "PC_TBPC_PurOrderprint.aspx?orderno=" + sheetno;
        }
        function openclosemodewin() {
            var orderno = "<%=gloabsheetno%>";
            var autonum = Math.round(10000 * Math.random());
            window.showModalDialog('PC_TBPC_PurOrder_close.aspx?autonum=' + autonum + '&orderno=' + orderno + '', '', "dialogHeight:800px;dialogWidth:1300px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            //               window.location.href="PC_TBPC_PurOrder.aspx?gloabsheetno="+orderno+"";
            window.location.reload();
        }
        //   self.moveTo(0,0)
        //  self.resizeTo(screen.availWidth,screen.availHeight)
    </script>

    <script language="javascript" type="text/javascript">
        //       Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        //        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        //        function BeginRequestHandler(sender, args)
        //        {
        //            
        //        }
        //        function EndRequestHandler(sender, args)
        //        {
        //         var myST = new superTable("tab", {
        //	        cssSkin : "sDefault",
        //	        headerRows : 1,
        //	        fixedCols : 0,
        ////	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
        //	        onStart : function () 
        //	        {
        //	            var ptc="<%=gloabptc%>";
        //                if(ptc!="")
        //                {
        //                    var table=document.getElementById("tab"); 
        //                    var tr=table.getElementsByTagName("tr"); 
        //                    for( i=1;i <tr.length-2;i++) 
        //                    {
        //                        var Qptc=tr[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
        //                        if(Qptc==ptc)
        //                        { 
        //                            tr[i].style.backgroundColor ='Orange'; 
        //                        }
        //                        else 
        //                        {
        //                            tr[i].style.backgroundColor ='';
        //                        }
        //                     }  
        //                 }      
        //	        },
        //	        onFinish : function () 
        //	        {   
        //	           
        //            }
        //        });
        //     }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btn_AddHTSP" runat="server" Text="生成采购合同" ForeColor="DodgerBlue"
                                Font-Bold="true" OnClick="btn_AddHTSP_Click" />&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_shanchu" runat="server" Text="删除" OnClick="btn_shanchu_Click"
                                Visible="false" />
                            <asp:Button ID="btn_dhqr" runat="server" Text="到货确认" OnClick="btn_dhqr_Click" />
                            <asp:Button ID="btn_marrep" runat="server" Text="代用" OnClick="btn_marrep_Click" />
                            <asp:Button ID="btn_edit" runat="server" Text="编辑" OnClick="btn_edit_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btn_tijiao" runat="server" Text="提交" OnClick="btn_tijiao_Click" />
                            <asp:Button ID="btn_nosto" runat="server" Text="wukucun" OnClick="btn_nosto_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btn_close" runat="server" Text="关闭" OnClientClick="openclosemodewin()" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btn_finish" runat="server" Text="完结" OnClick="btn_finish_Click" Visible="false" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btn_baojian" runat="server" Text="报检" OnClick="btn_baojian_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_mianjian" runat="server" Text="免检" OnClick="btn_mianjian_Click"
                                OnClientClick="return confirm('你确定提交吗?');" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="导出Excel" OnClick="btn_export_Click" />&nbsp;&nbsp;
                            <asp:Button ID="goback" runat="server" Text="返回" OnClick="goback_Click" />&nbsp;&nbsp;
                            <asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif"
                                    title="打印" Visible="false" /></asp:HyperLink>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table width="100%">
                    <tr>
                        <td align="left" runat="server" style="border-bottom: solid 1px black;">
                            <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                            <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                            <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="font-size: x-large; text-align: center;" colspan="3">
                            采购订单
                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                        </td>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
                        </td>
                        <td style="width: 40%;" align="left">
                            &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                            <asp:HiddenField runat="server" ID="supplierid" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;版本号：<asp:Label ID="LabelVersionNo" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                            <asp:Label ID="Tb_note" runat="server"></asp:Label>
                        </td>
                        <td style="width: 40%;" align="left">
                            &nbsp;&nbsp;&nbsp;摘&nbsp;&nbsp;&nbsp;要：<asp:Label ID="LabelAbstract" runat="server"></asp:Label>
                        </td>
                        <td id="Td6" runat="server" visible="false">
                            变更日期：<asp:Label ID="LabelChangeDate" runat="server"></asp:Label>
                            &nbsp;&nbsp;&nbsp;变更人：<asp:Label ID="LabelChangeMan" runat="server"></asp:Label>
                            变更原因：<asp:Label ID="LabelChangeReason" runat="server"></asp:Label>
                            <asp:TextBox ID="tb_state" runat="server" Text="" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="tb_cstate" runat="server" Text="" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height: 390px; overflow: auto; width: 100%">
                <div class="cpbox3 xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater ID="tbpc_order_detailRepeater" runat="server" OnItemDataBound="tbpc_order_detailRepeater_ItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <strong>行号</strong>
                                    </td>
                                    <td>
                                        <strong>计划跟踪号</strong>
                                    </td>
                                    <td>
                                        <strong>部件名称</strong>
                                    </td>
                                    <td>
                                        <strong>部件图号</strong>
                                    </td>
                                    <td>
                                        <strong>国标</strong>
                                    </td>
                                    <td>
                                        <strong>图号/标识号</strong>
                                    </td>
                                    <td>
                                        <strong>名称</strong>
                                    </td>
                                    <td>
                                        <strong>规格</strong>
                                    </td>
                                    <td>
                                        <strong>材质</strong>
                                    </td>
                                    <td>
                                        <strong>长度</strong>
                                    </td>
                                    <td>
                                        <strong>宽度</strong>
                                    </td>
                                    <td>
                                        <strong>单位</strong>
                                    </td>
                                    <td>
                                        <strong>数量</strong>
                                    </td>
                                    <td>
                                        <strong>辅助单位</strong>
                                    </td>
                                    <td>
                                        <strong>辅助数量</strong>
                                    </td>
                                    <td>
                                        <strong>含税单价</strong>
                                    </td>
                                    <td>
                                        <strong>加税合计</strong>
                                    </td>
                                    <td>
                                        <strong>单价(不含税)</strong>
                                    </td>
                                    <td>
                                        <strong>金额(不含税)</strong>
                                    </td>
                                    <td>
                                        <strong>交货日期</strong>
                                    </td>
                                    <td id="Td4" runat="server" visible="false">
                                        <strong>采购方式</strong>
                                    </td>
                                    <td>
                                        <strong>实际交货日期</strong>
                                    </td>
                                    <td>
                                        <strong>类型</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                    <td>
                                        <strong>税率(%)</strong>
                                    </td>
                                    <td>
                                        <strong>执行数量</strong>
                                    </td>
                                    <td>
                                        <strong>已到货数量</strong>
                                    </td>
                                    <td>
                                        <strong>片/支</strong>
                                    </td>
                                    <td runat="server" visible="false">
                                        <strong>计划模式</strong>
                                    </td>
                                    <td id="td1" visible="false" runat="server">
                                        <strong>计划跟踪号</strong>
                                    </td>
                                    <td runat="server" visible="false">
                                        <strong>关键部件</strong>
                                    </td>
                                    <td id="Th5" visible="false" runat="server">
                                        <strong>审核标志</strong>
                                    </td>
                                    <td>
                                        <strong>关闭标志</strong>
                                    </td>
                                    <td id="td2" visible="false" runat="server">
                                        <strong>比价单号</strong>
                                    </td>
                                    <td id="td3" runat="server" style="display: none">
                                        <strong>项目/工程</strong>
                                    </td>
                                    <td>
                                        <strong>销售合同号</strong>
                                    </td>
                                    <td>
                                        <strong>材料编码</strong>
                                    </td>
                                    <%-- <td id="td4" runat="server" style="display: none">
                                    <strong>计划时间</strong>
                                </td>--%>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                    <td runat="server" id="ch">
                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                            Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                        <asp:Label ID="lbUrgency" runat="server" Text="加急" Visible="false" ForeColor="Red" />
                                        <asp:Label ID="IFFAST" runat="server" Text='<%#Eval("IFFAST")%>' Visible="false"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="PlanCode" runat="server" Text='<%#Eval("PlanCode")%>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="txt_PlanCode" runat="server" Text='<%#Eval("PlanCode")%>' BorderStyle="None"
                                            Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("PlanCode")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="PO_CHILDENGNAME" runat="server" Text='<%#Eval("PO_CHILDENGNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PO_MAP" runat="server" Text='<%#Eval("PO_MAP")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MaterialGb" runat="server" Text='<%#Eval("MaterialGb")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PO_TUHAO" runat="server" Text='<%#Eval("PO_TUHAO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MaterialName" runat="server" Text='<%#Eval("MaterialName")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MaterialStandard" runat="server" Text='<%#Eval("MaterialStandard")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MaterialTexture" runat="server" Text='<%#Eval("MaterialTexture")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Length" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Width" runat="server" Text='<%#Eval("Width")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Unit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Number" runat="server" Text='<%#Eval("Number")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="PO_TECUNIT" runat="server" Text='<%#Eval("PO_TECUNIT")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="zxFznum" runat="server" Text='<%#Eval("zxFznum")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="CTUP" runat="server" Text='<%#Eval("CTUP")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="PricePlusTax" runat="server" Text='<%#Eval("PricePlusTax")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="UnitPrice" runat="server" Text='<%#Eval("UnitPrice")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Amount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Cgtimerq" runat="server" Text='<%#Eval("Cgtimerq")%>'></asp:Label>
                                    </td>
                                    <td id="Td7" runat="server" visible="false">
                                        <asp:Label ID="PO_CGFS" runat="server" Text='<%#Eval("PO_CGFS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Rptime" runat="server" Text='<%#Eval("Rptime")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="PO_MASHAPE" runat="server" Text='<%#Eval("PO_MASHAPE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Comment" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="TaxRate" runat="server" Text='<%#Eval("TaxRate")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="zxnum" runat="server" Text='<%#Eval("zxnum")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="arrivedNumber" runat="server" Text='<%#Eval("arrivedNumber")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PO_PZ" runat="server" Text='<%#Eval("PO_PZ")%>'></asp:Label>
                                    </td>
                                    <td runat="server" visible="false">
                                        <asp:Label ID="PlanMode" runat="server" Text='<%#Eval("PlanMode")%>'></asp:Label>
                                    </td>
                                    <td id="Td1" visible="false" runat="server">
                                        <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                    </td>
                                    <td runat="server" visible="false">
                                        <asp:Label ID="keycoms" runat="server" Text='<%#Eval("keycoms")%>'></asp:Label>&nbsp;
                                    </td>
                                    <td id="td5" visible="false" runat="server">
                                        <asp:Label ID="PO_STATE" runat="server" Text='<%#Eval("PO_STATE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="pocstatetrxt" runat="server" Text='<%#get_po_state(Eval("pocstate").ToString())%>'></asp:Label>
                                        <asp:Label ID="pocstate" runat="server" Text='<%#Eval("pocstate")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td id="Td2" visible="false" runat="server">
                                        <asp:Label ID="irqsheet" runat="server" Text='<%#Eval("irqsheet")%>'></asp:Label>
                                    </td>
                                    <td id="td3" runat="server" style="display: none">
                                        <asp:Label ID="pjid" runat="server" Text='<%#Eval("pjid")%>'></asp:Label>
                                        <asp:Label ID="engid" runat="server" Text='<%#Eval("engid")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="salescontract" runat="server" ForeColor="tomato"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MaterialCode" runat="server" Text='<%#Eval("MaterialCode")%>'></asp:Label>
                                    </td>
                                    <%--<td id="td4" runat="server" style="display: none">
                                    <asp:Label ID="plantime" runat="server" Text='<%#Eval("plantime")%>'></asp:Label>
                                </td>--%>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr align="center">
                                    <td colspan="13">
                                        汇&nbsp; &nbsp;总:
                                    </td>
                                    <td>
                                        <asp:Label ID="totalnum" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="total" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="1">
                                        &nbsp;
                                    </td>
                                    <td rowspan="1">
                                        <asp:Label ID="Labeltotal10" runat="server" Text=""></asp:Label>&nbsp;
                                    </td>
                                    <td colspan="5">
                                    </td>
                                    <td>
                                        <asp:Label ID="zxnum" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="recievenum" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="3">
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="30" align="center">
                                <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                    没有数据！</asp:Panel>
                            </td>
                        </tr>
                    </table>

                    <script language="javascript" type="text/javascript">
                        //        var myST = new superTable("tab", {
                        //	        cssSkin : "sDefault",
                        //	        headerRows : 1,
                        //	        fixedCols : 0,
                        ////	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                        //	        onStart : function () 
                        //	        {
                        //	            var ptc="<%=gloabptc%>";
                        //                if(ptc!="")
                        //                {
                        //                    var table=document.getElementById("tab"); 
                        //                    var tr=table.getElementsByTagName("tr"); 
                        //                    for( i=1;i <tr.length-2;i++) 
                        //                    {
                        //                        var Qptc=tr[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        //                        if(Qptc==ptc)
                        //                        { 
                        //                            tr[i].style.backgroundColor ='Orange'; 
                        //                        }
                        //                        else 
                        //                        {
                        //                            tr[i].style.backgroundColor ='';
                        //                        }
                        //                     }  
                        //                 }      
                        ////		        this.start = new Date();
                        //	        },
                        //	        onFinish : function () 
                        //	        {   
                        //	           
                        //            }
                        //        });
                    </script>

                </div>
            </div>
        </div>
        <asp:Panel ID="FooterPanel" runat="server" Width="100%">
            <table width="100%">
                <tr>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;主&nbsp;&nbsp;&nbsp;管：<asp:Label ID="LabelManager" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:Label ID="LabelDep" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;业务员：<asp:Label ID="LabelClerk" runat="server"></asp:Label>
                    </td>
                    <td style="width: 25%;" align="left">
                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDocument" runat="server"></asp:Label>
                        <asp:Label ID="LabelDocumentid" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
