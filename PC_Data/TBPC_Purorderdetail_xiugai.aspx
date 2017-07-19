<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="TBPC_Purorderdetail_xiugai.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.TBPC_Purorderdetail_xiugai"
    Title="订单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    编辑
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/Datetime.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/copy.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">
        function document.onreadystatechange() {
            if (document.readyState == "complete") {
                var ptc = "<%=gloabptc%>";
                var col = 2;
                othptcfind(ptc, col)
            }
        }
        function getClientId() {
            var paraId1 = '<%= LabelDate.ClientID %>';
            return { Id1: paraId1 };
        }
    </script>

    <script language="javascript" type="text/javascript">
     //执行辅助数量改变引起执行数量改变
        function amountcheckzxfzsl(obj) 
    {         
            var zxsl=obj.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;    
            var zxfzsl=obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
            var llzl=obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[1].value;
            var guige=obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[2].value;
           var lx=obj.parentNode.parentNode.getElementsByTagName("td")[23].getElementsByTagName("span")[0].innerHTML; 
           var unit=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("span")[0].innerHTML;
           var fzunit=obj.parentNode.parentNode.getElementsByTagName("td")[21].getElementsByTagName("span")[0].innerHTML;
            if(zxsl!=null&&zxfzsl!=null&&llzl!=null&&lx!=null&&zxsl!=0&&zxfzsl!=0&&llzl!=0)
            {
              if((lx=="板"||lx=="定尺板"||lx=="非定尺版")&&unit=="T")
              {
                  zxsl=(zxfzsl*llzl*guige/1000).toFixed(4);
                  obj.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=zxsl;
              }
              else if((lx=="型"||lx=="圆"||lx=="型材")&&unit=="T")
              {
                  zxsl=(zxfzsl*llzl/1000).toFixed(4);
                  obj.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=zxsl;
              }
              else if((lx=="板"||lx=="定尺板"||lx=="非定尺版")&&(unit=="m2"||unit=="M2"||unit=="平米")&&fzunit=="T")
              {
                  zxsl=(zxfzsl*1000/(llzl*guige)).toFixed(4);
                  obj.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=zxsl;
              }
            }
    }
    //执行数量改变引起执行辅助数量改变
    function amountcheckzxsl(obj) 
    {             
    
            var zxsl=obj.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;    
            var zxfzsl=obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
            var llzl=obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[1].value;
            var guige=obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[2].value;
            var lx=obj.parentNode.parentNode.getElementsByTagName("td")[23].getElementsByTagName("span")[0].innerHTML;
            var unit=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("span")[0].innerHTML;
            var fzunit=obj.parentNode.parentNode.getElementsByTagName("td")[21].getElementsByTagName("span")[0].innerHTML;       
            if(zxsl!=null&&zxfzsl!=null&&llzl!=null&&lx!=null&&zxsl!=0&&zxfzsl!=0&&llzl!=0)
            {
               if((lx=="板"||lx=="定尺板"||lx=="非定尺版")&&unit=="T")
              {
                  zxfzsl=(zxsl*1000/(llzl*guige)).toFixed(4);
                  obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=zxfzsl;
              }
              else if((lx=="型"||lx=="圆"||lx=="型材")&&unit=="T")
              {
                  zxfzsl=(zxsl*1000/llzl).toFixed(4);
                  obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=zxfzsl;
              }
              else if((lx=="板"||lx=="定尺板"||lx=="非定尺版")&&(unit=="m2"||unit=="M2"||unit=="平米")&&fzunit=="T")
              {
                  zxfzsl=(zxsl*llzl*guige/1000).toFixed(4);
                  obj.parentNode.parentNode.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=zxfzsl;
              }
            }
           
    }
    
    </script>
    <script type="text/javascript">
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
    </script>
    <div class="RightContent">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        采购订单
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="confirm" runat="server" Text="保存" OnClick="save_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_hclose" runat="server" Text="行关闭" OnClick="btn_hclose_Click"
                                            Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <%--<asp:Button ID="btn_bk" runat="server" Text="备库"/>&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                        <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="goback" runat="server" Text="返回" OnClick="goback_Click" />&nbsp;&nbsp;&nbsp;
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
                                    <td style="font-size: x-large; text-align: center;" colspan="3">
                                        采购订单
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="color: Red">注：交货日期手动输入第一行，可带出下面的日期</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;" align="left">
                                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 30%;" align="left">
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 400%;" align="left">
                                        &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td6" style="width: 30%;" align="left" runat="server">
                                        &nbsp;&nbsp;&nbsp;版本号：<asp:Label ID="LabelVersionNo" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 30%;">
                                        &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                                        <asp:TextBox ID="Tb_note" runat="server" Text=""></asp:TextBox>
                                    </td>
                                    <td id="Td7" style="width: 40%;" align="left" runat="server">
                                        &nbsp;&nbsp;&nbsp;摘&nbsp;&nbsp;&nbsp;要：
                                        <asp:TextBox ID="tb_Abstract" runat="server"></asp:TextBox>
                                    </td>
                                    <td id="Td8" runat="server" visible="false">
                                        变更日期：<asp:Label ID="LabelChangeDate" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;变更人：<asp:Label ID="LabelChangeMan" runat="server"></asp:Label>
                                        变更原因：
                                        <asp:TextBox ID="tb_ChangeReason" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnqrfp" runat="server" Text="按比例分配" OnClick="btnqrfp_click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnxiugaitixing" runat="server" Text="数据修改邮件提醒" OnClientClick="viewCondition()" />
                                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnxiugaitixing"
                                            PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                            Y="200" X="350">
                                        </asp:ModalPopupExtender>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="PanelCondition" runat="server" Width="320px" Style="display: none">
                                <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="320px" style="background-color: #CCCCFF; border: solid 1px black;">
                                            <tr>
                                                <td colspan="8" align="center">
                                                    <asp:Button ID="btnqueren" runat="server" OnClick="btnqueren_Click" Text="确认" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnquxiao" runat="server" OnClick="btnquxiao_Click" Text="取消" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    修改原因及内容：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbreason" runat="server" Width="230px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                           </asp:Panel>
                        </div>
                        <div style="border: 1px solid #000000; height: 360px;">
                            <div class="cpbox7 xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="PurorderdetailRepeater" runat="server" OnItemDataBound="PurorderdetailRepeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>图号/标识号</strong>
                                                </td>
                                                <td>
                                                    <strong>物料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>物料名称</strong>
                                                </td>
                                                <td>
                                                    <strong>物料规格</strong>
                                                </td>
                                                <td>
                                                    <strong>物料材质</strong>
                                                </td>
                                                <td>
                                                    <strong>国标</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>交货日期</strong>
                                                </td>
                                                <td>
                                                    <strong>单价</strong>
                                                </td>
                                                <td>
                                                    <strong>金额</strong>
                                                </td>
                                                <td>
                                                    <strong>含税单价</strong>
                                                </td>
                                                <td>
                                                    <strong>税率(%)</strong>
                                                </td>
                                                <td>
                                                    <strong>价税合计</strong>
                                                </td>
                                                <td>
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>执行数量</strong>
                                                </td>
                                                <td>
                                                    <strong>已到货数量</strong>
                                                </td>
                                                <td>
                                                    <strong>实际交货日期</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>执行辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助单位</strong>
                                                </td>
                                                <td>
                                                    <strong>片/支</strong>
                                                </td>
                                                <td>
                                                    <strong>类型</strong>
                                                </td>
                                                <td>
                                                    <strong>长度</strong>
                                                </td>
                                                <td>
                                                    <strong>宽度</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>计划模式</strong>
                                                </td>
                                                <td id="Th1" visible="false" runat="server">
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>是否关键部件</strong>
                                                </td>
                                                <td id="Th5" visible="false" runat="server">
                                                    <strong>审核标志</strong>
                                                </td>
                                                <td>
                                                    <strong>计划号</strong>
                                                </td>
                                                <td>
                                                    <strong>关闭标志</strong>
                                                </td>
                                                <td id="Th2" visible="false" runat="server">
                                                    <strong>比价单</strong>
                                                </td>
                                                <td id="Th3" runat="server" style="display: none">
                                                    <strong>工程</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_TUHAO" runat="server" Text='<%#Eval("PO_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_MARID" runat="server" Text='<%#Eval("PO_MARID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_MARNAME" runat="server" Text='<%#Eval("PO_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_GUIGE" runat="server" Text='<%#Eval("PO_GUIGE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_CAIZHI" runat="server" Text='<%#Eval("PO_CAIZHI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_GUOBIAO" runat="server" Text='<%#Eval("PO_GUOBIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_UNIT" runat="server" Text='<%#Eval("PO_UNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PO_CGTIMERQ" runat="server" Text='<%#Eval("PO_CGTIMERQ")%>' Width="90px"
                                                        OnTextChanged="PO_CGTIMERQ_Textchanged" AutoPostBack="true"></asp:TextBox>
                                                    
                                                    <cc1:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                                        TargetControlID="PO_CGTIMERQ">
                                                    </cc1:CalendarExtender>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("PO_CGTIMERQ")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_UPRICE" runat="server" Text='<%#Eval("PO_UPRICE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_AMOUNT" runat="server" Text='<%#Eval("PO_AMOUNT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_CTAXUPRICE" runat="server" Text='<%#Eval("PO_CTAXUPRICE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_TAXRATE" runat="server" Text='<%#Eval("PO_TAXRATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_TOTALPRICE" runat="server" Text='<%#Eval("PO_TOTALPRICE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_QUANTITY" runat="server" Text='<%#Eval("PO_QUANTITY")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PO_ZXNUM" runat="server" Text='<%#Eval("PO_ZXNUM")%>' BorderStyle="None"
                                                        Width="60px" onkeydown="grControlFocus1(this)" onchange="amountcheckzxsl(this)"></asp:TextBox>
                                                        <%--<asp:HiddenField ID="hdzxxj" runat="server" Value='<%#Eval("PO_ZXNUMXJ")%>' />--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_RECGDNUM" runat="server" Text='<%#Eval("PO_RECGDNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_RECDATE" runat="server" Text='<%#Eval("PO_RECDATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_FZNUM" runat="server" Text='<%#Eval("PO_FZNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PO_ZXFZNUM" runat="server" Text='<%#Eval("PO_ZXFZNUM")%>' BorderStyle="None"
                                                        Width="80px"  onkeydown="grControlFocus1(this)" onchange="amountcheckzxfzsl(this)"></asp:TextBox>
                                                    <asp:HiddenField ID="lbllzl" runat="server" Value='<%#Eval("MWEIGHT")%>' />
                                                    <asp:HiddenField ID="lbguige" runat="server" Value='<%#Eval("GUIGE")%>' />
<%--                                                    <asp:HiddenField ID="hdzxfzxj" runat="server" Value='<%#Eval("PO_ZXFZNUMXJ")%>' />--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_FZUNIT" runat="server" Text='<%#Eval("PO_FZUNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PO_PZ" runat="server" Text='<%#Eval("PO_PZ")%>' BorderStyle="None"
                                                        Width="80px" onkeydown="grControlFocus1(this)"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_MASHAPE" runat="server" Text='<%#Eval("PO_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_LENGTH" runat="server" Text='<%#Eval("PO_LENGTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_WIDTH" runat="server" Text='<%#Eval("PO_WIDTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PO_NOTE" runat="server" Text='<%#Eval("PO_NOTE")%>' BorderStyle="None"
                                                        onkeydown="grControlFocus1(this)"></asp:TextBox>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="PO_PMODE" runat="server" Text='<%#Eval("PO_PMODE")%>'></asp:Label>
                                                </td>
                                                <td id="td1" visible="false" runat="server">
                                                    <asp:Label ID="PO_PTCODE" runat="server" Text='<%#Eval("PO_PTCODE")%>'></asp:Label>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="PO_KEYCOMS" runat="server" Text='<%#Eval("PO_KEYCOMS")%>'></asp:Label>
                                                </td>
                                                <td id="td5" visible="false" runat="server">
                                                    <asp:Label ID="PO_STATETEXT" runat="server" Text='<%#get_po_state(Eval("PO_STATE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PO_STATE" runat="server" Text='<%#Eval("PO_STATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_PCODE" runat="server" Text='<%#Eval("PO_PCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PO_CSTATETEXT" runat="server" Text='<%#get_po_cstate(Eval("PO_CSTATE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PO_CSTATE" runat="server" Text='<%#Eval("PO_CSTATE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td id="td2" visible="false" runat="server">
                                                    <asp:Label ID="PO_ICLSHEETNO" runat="server" Text='<%#Eval("PO_ICLSHEETNO")%>'></asp:Label>
                                                </td>
                                                <td id="td3" runat="server" style="display: none">
                                                    <asp:Label ID="PO_ENGID" runat="server" Text='<%#Eval("PO_ENGID")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr align="center">
                                                <td colspan="11">
                                                    汇&nbsp; &nbsp;总:
                                                </td>
                                                <td rowspan="1">
                                                    <asp:Label ID="Labeltotal" runat="server" Text=""></asp:Label>&nbsp;
                                                </td>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Labeltotal10" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="totalnum" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zxnum" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td colspan="13">
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
                                    //                                    var myST = new superTable("tab", {
                                    //                                        cssSkin: "tDefault",
                                    //                                        headerRows: 1,
                                    //                                        fixedCols: 0,
                                    //                                        //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                    //                                        onStart: function() {
                                    //                                            //		        this.start = new Date();
                                    //                                        },
                                    //                                        onFinish: function() {
                                    //                                            //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                                    //                                        }
                                    //                                    });
                                </script>

                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="FooterPanel" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;" align="left">
                                    &nbsp;&nbsp;&nbsp;主&nbsp;&nbsp;&nbsp;管：
                                    <cc1:ComboBox ID="cob_zg" runat="server" Height="15px" Width="120px" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList">
                                    </cc1:ComboBox>
                                </td>
                                <td style="width: 25%;" align="left">
                                    &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：
                                    <asp:TextBox ID="tbDep" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:Label ID="lbDepID" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 25%;" align="left">
                                    &nbsp;&nbsp;&nbsp;业务员：
                                    <cc1:ComboBox ID="cob_ywy" runat="server" Height="15px" Width="120px" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList">
                                    </cc1:ComboBox>
                                </td>
                                <td style="width: 25%;" align="left">
                                    &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：
                                    <asp:TextBox ID="tbDocument" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:Label ID="lbDocumentID" runat="server" Text="" Visible="false"></asp:Label>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
