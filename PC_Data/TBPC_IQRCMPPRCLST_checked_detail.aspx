<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="TBPC_IQRCMPPRCLST_checked_detail.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.TBPC_IQRCMPPRCLST_checked_detail"
    Title="比价单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价单审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../PC_Data/PcJs/pricesearch.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function openLink(obj) {
            var name;
            name = obj.getElementsByTagName("td")[4].getElementsByTagName("span")[0].innerHTML;
            windowopen("TBPC_ShowSupply.aspx?name=" + name);

        }

        function sinCheck(obj) {
            if (obj.checked) {
                $(":checkbox", $(".zbjc")).attr("checked", false);
                $(obj).attr("checked", true);
            };
        }
        
                function req_zero(){
           var SfCzbfgxint="<%=Get_zero_itemcount()%>";
            if(SfCzbfgxint!=0)
            {
            if(window.confirm('存在单价为0的项，若为成套购买请做标识；确定将直接提交，是否确定！')){ 
                return true; 
                }else{  
                return false; 
                } 
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        //    function BJDchaifen()
        //    {
        //       var shape="<%=gloabshape%>";
        //       var ptcode="<%=gloabptcode%>";
        //       var autonum=Math.round(10000*Math.random());    
        ////    showModalDialog
        //       var aa=window.open("PC_Date_BJDchaifen.aspx?autonum="+autonum+"&shape="+escape(shape)+"&ptcode="+escape(ptcode),'obj',"dialogHeight:500px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        //       if(aa=="Refesh")
        //       {
        //           window.returnValue="Refesh";
        //           window.location.reload();
        //       }
        //    }
        //    self.moveTo(0,0)
        //    self.resizeTo(screen.availWidth,screen.availHeight)
    </script>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 4,
                fixedCols: 9,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    var ptc = "<%=gloabptc%>";
                    if (ptc != "") {
                        var col = 2;
                        var table = document.getElementById("tab");
                        var tr = table.getElementsByTagName("tr");
                        for (i = 4; i < tr.length - 2; i++) {
                            var Qptc = tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                            if (Qptc.substring(0, Qptc.indexOf("#")) != "" && ptc.substring(0, ptc.indexOf("#")) == "") {
                                Qptc = Qptc.substring(0, Qptc.indexOf("#"));
                            }
                            if (Qptc == ptc) {
                                tr[i].style.backgroundColor = 'Orange';
                            }
                            else {
                                tr[i].style.backgroundColor = '';
                            }
                        }
                    }
                },
                onFinish: function() {
                    for (var i = 0, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {

                        this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function(i) {
                            var clicked = false;

                            var dataRow = this.sDataTable.tBodies[0].rows[i];
                            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                            return function() {
                                if (clicked) {
                                    dataRow.style.backgroundColor = "#ffffff";
                                    fixedRow.style.backgroundColor = "#C6E2FF";
                                    clicked = false;
                                }
                                else {
                                    dataRow.style.backgroundColor = "#FFC125";
                                    fixedRow.style.backgroundColor = "#FFC125";
                                    clicked = true;
                                }
                            }
                        } .call(this, i);
                    }
                    return this;
                }
            });
        }
    </script>

    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                询比价单
                            </td>
                            <td align="right" width="32%">
                                <asp:Button ID="btn_fuzhi" runat="server" Text="复制" OnClick="btn_fuzhi_Click" Visible="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_chaifen" runat="server" Text="拆分" OnClick="btn_chaifen_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_yincang" runat="server" Text="隐藏设置" OnClientClick="viewCondition()" />
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_yincang"
                                    PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    Y="80" CancelControlID="btnClose">
                                </asp:ModalPopupExtender>
                                &nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnReplace" OnClick="btnReplace_Click" Text="物料代用"
                                    Enabled="false" />
                            </td>
                            <td align="right">
                                <asp:CheckBox ID="Chb_Zb" Text="招标物料" runat="server" class="zbjc" onclick="sinCheck(this)" />&nbsp;
                                <asp:CheckBox ID="Chb_Jc" Text="集采采购" runat="server" class="zbjc" onclick="sinCheck(this)" />&nbsp;
                                <asp:CheckBox ID="chk_array" Text="成套标识" runat="server" OnCheckedChanged="chk_array_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chk_qxarray" Text="取消成套标识" runat="server" OnCheckedChanged="chk_qxarray_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="CancelZb" Text="取消招标" OnClick="CancelJc_Click" Visible="false" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="CancelJc" Text="取消集采" OnClick="CancelJc_Click" Visible="false" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="cbpower" runat="server" Text="招标审核" Enabled="false" OnCheckedChanged="cbpower_CheckedChanged"
                                    AutoPostBack="true" Visible="false" />
                                <asp:Button ID="btn_edit" runat="server" Text="编辑" OnClick="btn_edit_Click" Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="提  交" OnClick="btn_confirm_Click" OnClientClick="return req_zero()"
                                    Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_Click"
                                    Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_shangcha" runat="server" Text="上查" OnClick="btn_shangcha_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_biangeng" runat="server" Text="变更查询" OnClick="btn_biangeng_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_nosto" runat="server" Text="wukucun" OnClick="btn_nosto_Click" />
                                <asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                    <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif"
                                        title="打印" Visible="false" /></asp:HyperLink>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="60%" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="60%" style="background-color: #CCCCFF; border: solid 1px black;" border="1">
                                    <tr>
                                        <td colspan="4" align="center">
                                            选择需要隐藏的列<br />
                                            只隐藏价格输入界面上的列
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="计划跟踪号" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox2" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="物料编码" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox3" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="图号" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox4" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="材质" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox5" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="国标" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox6" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="采购数量" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox7" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="单位" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox8" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="税率" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox9" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="金额" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox10" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="长度" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox11" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="宽度" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox12" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="备注" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr visible="false">
                                        <td visible="false">
                                            <asp:CheckBox ID="CheckBox13" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="辅助数量" TextAlign="Right" Visible="false"></asp:CheckBox>
                                        </td>
                                        <td visible="false">
                                            <asp:CheckBox ID="CheckBox14" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="辅助单位" TextAlign="Right" Visible="false"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="确定" />
                                            &nbsp;&nbsp;&nbsp;
                                            <input type="button" id="btnClose" value="取消" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    询比价单编号:<asp:TextBox ID="TextBoxNO" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="right">
                                    显示供应商数量：<asp:TextBox ID="TextBox1" runat="server" Text="3"></asp:TextBox>&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="询比价单">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 480px">
                                        <div class="cpbox4 xscroll">
                                            <table id="tab" class="nowrap cptable fullwidth">
                                                <asp:Repeater ID="checked_detailRepeater" runat="server" OnItemDataBound="checked_detailRepeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr id="row" align="center" style="background-color: #B9D3EE">
                                                            <td id="wlxx" runat="server" rowspan="3">
                                                                物料信息
                                                            </td>
                                                            <td colspan="1" rowspan="3">
                                                                比价结果
                                                            </td>
                                                            <td id="GYS" runat="server" colspan="18" rowspan="1">
                                                                供应商信息
                                                            </td>
                                                        </tr>
                                                        <tr align="center" style="background-color: #B9D3EE">
                                                            <td id="gys1" runat="server" colspan="3" rowspan="1">
                                                                供应商1
                                                            </td>
                                                            <td id="gys2" runat="server" colspan="3" rowspan="1">
                                                                供应商2
                                                            </td>
                                                            <td id="gys3" runat="server" colspan="3" rowspan="1">
                                                                供应商3
                                                            </td>
                                                            <td id="gys4" runat="server" colspan="3" rowspan="1">
                                                                供应商4
                                                            </td>
                                                            <td id="gys5" runat="server" colspan="3" rowspan="1">
                                                                供应商5
                                                            </td>
                                                            <td id="gys6" runat="server" colspan="3" rowspan="1">
                                                                供应商6
                                                            </td>
                                                        </tr>
                                                        <tr align="left" style="background-color: #B9D3EE">
                                                            <td id="gysnm1" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERANAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERAID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbA_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td id="gysnm2" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERBNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERBID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbB_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td id="gysnm3" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERCNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERCID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbC_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td id="gysnm4" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERDNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERDID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbD_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td id="gysnm5" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERENAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIEREID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbE_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                            <td id="gysnm6" runat="server" bgcolor="#FFFFCC" colspan="3" rowspan="1" width="180px">
                                                                <asp:Label ID="PIC_SUPPLIERFNAME" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="PIC_SUPPLIERFID" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="LbF_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center" style="background-color: #B9D3EE">
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td id="td1" runat="server">
                                                                <strong>计划号跟踪号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>部件名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>部件图号</strong>
                                                            </td>
                                                            <td id="td5" runat="server">
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td id="td3" runat="server">
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td id="td13" runat="server" visible="false">
                                                                <strong>销售合同</strong>
                                                            </td>
                                                            <td>
                                                                <strong>规格</strong>
                                                            </td>
                                                            <td id="td4" runat="server">
                                                                <strong>材质</strong>
                                                            </td>
                                                            <td id="td9" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="td10" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <%-- <td>
                                                                <strong>数量</strong>
                                                            </td>--%>
                                                            <td id="td7" runat="server">
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td id="td6" runat="server">
                                                                <strong>采购数量</strong>
                                                            </td>
                                                            <td id="fzunit1" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td id="fznum1" runat="server">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <%--  <td>
                                                                <strong>税率(%)</strong>
                                                            </td>--%>
                                                            <td id="td8" runat="server">
                                                                <strong>金额</strong>
                                                            </td>
                                                            <%--<td id="zxfznum1" runat="server">
                                                                <strong>执行辅助数量</strong>
                                                            </td>--%>
                                                            <%-- <td>
                                                                <strong>计划模式</strong>
                                                            </td>--%>
                                                            <%-- <td>
                                                                <strong>是否关键部件</strong>
                                                            </td>--%>
                                                            <td id="td11" runat="server">
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td>
                                                                <strong>类型</strong>
                                                            </td>
                                                            <td>
                                                                <strong>申请人</strong>
                                                            </td>
                                                            <td id="td2" runat="server">
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td>
                                                                <strong>比价结果</strong>
                                                            </td>
                                                            <td id="dyc1" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec1" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="zz1" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc2" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec2" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="zz2" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc3" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec3" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="zz3" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc4" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec4" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="zz4" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc5" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec5" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="zz5" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc6" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec6" runat="server">
                                                                <strong>单价</strong>
                                                            </td>
                                                            <td id="zz6" runat="server">
                                                                <strong>金额</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <FooterTemplate>
                                                        <tr align="center">
                                                            <td id="foottd1" runat="server" rowspan="1">
                                                                汇总(RMB/元)
                                                            </td>
                                                            <td id="tk4" runat="server">
                                                                <%--长--%>
                                                                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk5" runat="server">
                                                                <%--宽--%>
                                                                <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk3" runat="server">
                                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk1" runat="server">
                                                                <asp:Label ID="Lb_zxnum" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td id="tk2" runat="server">
                                                                <asp:Label ID="Lb_amount" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="tk6" runat="server">
                                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td id="Td1" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal11" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="Td2" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal12" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="Td3" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal13" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="Td4" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal14" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="Td5" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal15" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td id="Td6" runat="server" align="center" colspan="3">
                                                                <asp:Label ID="Labeltotal16" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="sumzdjprice" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="sumzdjnum" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget">
                                                            <td>
                                                                <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" Checked="false"
                                                                    CssClass="checkBoxCss" onclick="checkme(this)" />
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" Text="<%#Container.ItemIndex + 1 %>"></asp:Label>
                                                            </td>
                                                            <td id="tt1" runat="server">
                                                                <asp:Label ID="PIC_PCODE" runat="server" Text='<%#Eval("ptcode")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_PCODE" BorderStyle="None" Text='<%#Eval("ptcode")%>'
                                                                    ToolTip='<%#Eval("ptcode")%>' Style="background-color: Transparent; text-align: center"></asp:TextBox>
                                                                <asp:Label runat="server" ID="hid_picode" Text='<%#Eval("PIC_PICODE") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_CHILDENGNAME" runat="server" Text='<%#Eval("PIC_CHILDENGNAME")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_CHILDENGNAME" BorderStyle="None" Text='<%#Eval("PIC_CHILDENGNAME")%>'
                                                                    ToolTip='<%#Eval("PIC_CHILDENGNAME")%>' Style="background-color: Transparent;
                                                                    text-align: center" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MAP" runat="server" Text='<%#Eval("PIC_MAP")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_MAP" BorderStyle="None" Text='<%#Eval("PIC_MAP")%>'
                                                                    ToolTip='<%#Eval("PIC_MAP")%>' Style="background-color: Transparent; text-align: center"
                                                                    Width="70px"></asp:TextBox>
                                                            </td>
                                                            <td id="tt5" runat="server">
                                                                <asp:Label ID="PIC_GUOBIAO" runat="server" Text='<%#Eval("margb")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_GUOBIAO" BorderStyle="None" Text='<%#Eval("margb")%>'
                                                                    ToolTip='<%#Eval("margb")%>' Style="background-color: Transparent; text-align: center"
                                                                    Width="70px"></asp:TextBox>
                                                            </td>
                                                            <td id="tt3" runat="server">
                                                                <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_TUHAO" BorderStyle="None" Text='<%#Eval("PIC_TUHAO")%>'
                                                                    ToolTip='<%#Eval("PIC_TUHAO")%>' Style="background-color: Transparent; text-align: center"
                                                                    Width="70px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MARNAME" runat="server" Text='<%#Eval("marnm")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_MARNAME" BorderStyle="None" Text='<%#Eval("marnm")%>'
                                                                    ToolTip='<%#Eval("marnm")%>' Style="background-color: Transparent; text-align: center"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td id="tt13" runat="server" visible="false">
                                                                <asp:Label ID="PIC_SALESCONTRACT" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MARGUIGE" runat="server" Text='<%#Eval("margg")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_MARGUIGE" BorderStyle="None" Text='<%#Eval("margg")%>'
                                                                    ToolTip='<%#Eval("margg")%>' Style="background-color: Transparent; text-align: center"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td id="tt4" runat="server">
                                                                <asp:Label ID="PIC_MARCAIZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt9" runat="server">
                                                                <asp:Label ID="PIC_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt10" runat="server">
                                                                <asp:Label ID="PIC_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt7" runat="server">
                                                                <asp:Label ID="PIC_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt6" runat="server">
                                                                <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="marfzunit" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="marfznum" runat="server" Text='<%#Eval("marfznum")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt8" runat="server">
                                                                <asp:Label ID="Amount" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt11" runat="server">
                                                                <asp:Label ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox runat="server" ID="txt_NOTE" BorderStyle="None" Text='<%#Eval("detailnote")%>'
                                                                    Width="120px" ToolTip='<%#Eval("detailnote")%>' Style="background-color: Transparent;
                                                                    text-align: center"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MASHAPE" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="ST_SQR" runat="server" Text='<%#Eval("ST_SQR")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt2" runat="server">
                                                                <asp:Label ID="PIC_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="supplierid" runat="server" Text='<%#Eval("supplierresid")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                            </td>
                                                            <td id="dycbj1" runat="server" width="60px">
                                                                <asp:Label ID="Label11" runat="server" Text='<%#Eval("qoutefstsa")%>'></asp:Label>
                                                            </td>
                                                            <td id="decbj1" runat="server" width="60px">
                                                                <asp:Label ID="Label12" runat="server" Text='<%#Eval("qoutescdsa")%>'></asp:Label>
                                                            </td>
                                                            <td id="zzbj1" runat="server" width="60px">
                                                                <asp:Label ID="Label13" runat="server" Text='<%#Eval("qoutelstsa")%>'></asp:Label>
                                                            </td>
                                                            <td id="dycbj2" runat="server" width="60px">
                                                                <asp:Label ID="Label21" runat="server" Text='<%#Eval("qoutefstsb")%>'></asp:Label>
                                                            </td>
                                                            <td id="decbj2" runat="server" width="60px">
                                                                <asp:Label ID="Label22" runat="server" Text='<%#Eval("qoutescdsb")%>'></asp:Label>
                                                            </td>
                                                            <td id="zzbj2" runat="server" width="60px">
                                                                <asp:Label ID="Label23" runat="server" Text='<%#Eval("qoutelstsb")%>'></asp:Label>
                                                            </td>
                                                            <td id="dycbj3" runat="server" width="60px">
                                                                <asp:Label ID="Label31" runat="server" Text='<%#Eval("qoutefstsc")%>'></asp:Label>
                                                            </td>
                                                            <td id="decbj3" runat="server" width="60px">
                                                                <asp:Label ID="Label32" runat="server" Text='<%#Eval("qoutescdsc")%>'></asp:Label>
                                                            </td>
                                                            <td id="zzbj3" runat="server" width="60px">
                                                                <asp:Label ID="Label33" runat="server" Text='<%#Eval("qoutelstsc")%>'></asp:Label>
                                                            </td>
                                                            <td id="dycbj4" runat="server" width="60px">
                                                                <asp:Label ID="Label41" runat="server" Text='<%#Eval("qoutefstsd")%>'></asp:Label>
                                                            </td>
                                                            <td id="decbj4" runat="server" width="60px">
                                                                <asp:Label ID="Label42" runat="server" Text='<%#Eval("qoutescdsd")%>'></asp:Label>
                                                            </td>
                                                            <td id="zzbj4" runat="server" width="60px">
                                                                <asp:Label ID="Label43" runat="server" Text='<%#Eval("qoutelstsd")%>'></asp:Label>
                                                            </td>
                                                            <td id="dycbj5" runat="server" width="60px">
                                                                <asp:Label ID="Label51" runat="server" Text='<%#Eval("qoutefstse")%>'></asp:Label>
                                                            </td>
                                                            <td id="decbj5" runat="server" width="60px">
                                                                <asp:Label ID="Label52" runat="server" Text='<%#Eval("qoutescdse")%>'></asp:Label>
                                                            </td>
                                                            <td id="zzbj5" runat="server" width="60px">
                                                                <asp:Label ID="Label53" runat="server" Text='<%#Eval("qoutelstse")%>'></asp:Label>
                                                            </td>
                                                            <td id="dycbj6" runat="server" width="60px">
                                                                <asp:Label ID="Label61" runat="server" Text='<%#Eval("qoutefstsf")%>'></asp:Label>
                                                            </td>
                                                            <td id="decbj6" runat="server" width="60px">
                                                                <asp:Label ID="Label62" runat="server" Text='<%#Eval("qoutescdsf")%>'></asp:Label>
                                                            </td>
                                                            <td id="zzbj6" runat="server" width="60px">
                                                                <asp:Label ID="Label63" runat="server" Text='<%#Eval("qoutelstsf")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="36">
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="False">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>

                                            <script language="javascript" type="text/javascript">

                                                var k = 9;
                                                var j = 15;
                                                if (document.getElementById("<%=CheckBox1.ClientID%>").checked) {
                                                    k--;
                                                    j--;
                                                }
                                                if (document.getElementById("<%=CheckBox2.ClientID%>").checked) {
                                                    k--;
                                                    j--;
                                                }
                                                if (document.getElementById("<%=CheckBox3.ClientID%>").checked) {
                                                    k--;
                                                    j--;
                                                }
                                                if (document.getElementById("<%=CheckBox4.ClientID%>").checked) {
                                                    k--;
                                                    j--;
                                                }
                                                var myST = new superTable("tab", {
                                                    cssSkin: "sDefault",
                                                    headerRows: 4,
                                                    fixedCols: k,
                                                    //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                                    onStart: function() {
                                                        var ptc = "<%=gloabptc%>";
                                                        if (ptc != "") {
                                                            var col = 2;
                                                            var table = document.getElementById("tab");
                                                            var tr = table.getElementsByTagName("tr");
                                                            for (i = 4; i < tr.length - 2; i++) {
                                                                var Qptc = tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                                                                if (Qptc.substring(0, Qptc.indexOf("#")) != "" && ptc.substring(0, ptc.indexOf("#")) == "") {
                                                                    Qptc = Qptc.substring(0, Qptc.indexOf("#"));
                                                                }
                                                                if (Qptc == ptc) {
                                                                    tr[i].style.backgroundColor = 'Orange';
                                                                }
                                                                else {
                                                                    tr[i].style.backgroundColor = '';
                                                                }
                                                            }
                                                        }
                                                        //		        this.start = new Date();
                                                    },
                                                    onFinish: function() {
                                                        for (var i = 0, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {

                                                            this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function(i) {
                                                                var clicked = false;

                                                                var dataRow = this.sDataTable.tBodies[0].rows[i];
                                                                var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                                                                return function() {
                                                                    if (clicked) {
                                                                        dataRow.style.backgroundColor = "#ffffff";
                                                                        fixedRow.style.backgroundColor = "#C6E2FF";
                                                                        clicked = false;
                                                                    }
                                                                    else {
                                                                        dataRow.style.backgroundColor = "#FFC125";
                                                                        fixedRow.style.backgroundColor = "#FFC125";
                                                                        clicked = true;
                                                                    }
                                                                }
                                                            } .call(this, i);
                                                        }
                                                        return this;
                                                    }
                                                });
                                            </script>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="比价结果" TabIndex="1">
                                <ContentTemplate>
                                    <div class="fixbox" style="height: 440px">
                                        <table id="Table1" class="nowrap fixtable fullwidth" width="100%">
                                            <asp:Repeater ID="tbpc_comparepriceresultRepeater" runat="server" OnItemDataBound="tbpc_comparepriceresultRepeater_ItemDataBound">
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
                                                        <td runat="server" visible="false">
                                                            <strong>销售合同</strong>
                                                        </td>
                                                        <td>
                                                            <strong>物料编码</strong>
                                                        </td>
                                                        <td>
                                                            <strong>名称</strong>
                                                        </td>
                                                        <td>
                                                            <strong>图号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>规格</strong>
                                                        </td>
                                                        <td>
                                                            <strong>材质</strong>
                                                        </td>
                                                        <td>
                                                            <strong>国标</strong>
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <strong>数量</strong>
                                                        </td>
                                                        <td>
                                                            <strong>采购数量</strong>
                                                        </td>
                                                        <td>
                                                            <strong>单位</strong>
                                                        </td>
                                                        <td>
                                                            <strong>供应商</strong>
                                                        </td>
                                                        <td>
                                                            <strong>交货期</strong>
                                                        </td>
                                                        <td>
                                                            <strong>单价(含税)</strong>
                                                        </td>
                                                        <td>
                                                            <strong>金额</strong>
                                                        </td>
                                                        <td>
                                                            <strong>税率(%)</strong>
                                                        </td>
                                                        <td id="fzn1" runat="server">
                                                            <strong>辅助数量</strong>
                                                        </td>
                                                        <td id="zxfzn1" runat="server">
                                                            <strong>执行辅助数量</strong>
                                                        </td>
                                                        <td id="fzu1" runat="server">
                                                            <strong>辅助单位</strong>
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
                                                        <td>
                                                            <strong>关闭标志</strong>
                                                        </td>
                                                        <td>
                                                            <strong>成套标识</strong>
                                                        </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseout="this.className='baseGadget'" onmouseover="this.className='highlight'">
                                                        <td>
                                                            <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" Checked="false"
                                                                CssClass="checkBoxCss" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="rownum" runat="server" Text="<%#Container.ItemIndex + 1 %>"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_PCODE" runat="server" Text='<%#Eval("ptcode")%>' Visible="false"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txt_ptcode" BorderStyle="None" Text='<%#Eval("ptcode")%>'
                                                                ToolTip='<%#Eval("ptcode")%>' Style="background-color: Transparent; text-align: center"></asp:TextBox>
                                                            &nbsp;
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="PIC_SALESCONTRACT" runat="server"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARGUIGE" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_MARCAIZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="PIC_QUANTITY" runat="server" Text='<%#Eval("marnum")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="supplierid" runat="server" Text='<%#Eval("supplierresid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_PRICE" runat="server" Text='<%#Eval("price")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Amount" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_SHUILV" runat="server" Text='<%#Eval("shuilv")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="fzn2" runat="server">
                                                            <asp:Label ID="PIC_FZNUM" runat="server" Text='<%#Eval("marfznum")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="zxfzn2" runat="server">
                                                            <asp:Label ID="PIC_ZXFUNUM" runat="server" Text='<%#Eval("marzxfznum")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td id="fzu2" runat="server">
                                                            <asp:Label ID="PIC_FZUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="hgbstate" runat="server" Text='<%#get_pur_hgb(Eval("detailcstate").ToString())%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbarray" runat="server" Text='<%#Eval("PIC_ARRAY")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr align="center">
                                                        <td colspan="10" rowspan="1">
                                                            汇总(RMB/元)
                                                        </td>
                                                        <td runat="server" visible="false">
                                                            <asp:Label ID="totalnum" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="totalzxnum" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td colspan="4">
                                                        </td>
                                                        <td rowspan="1">
                                                            <asp:Label ID="totalamount" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td id="foot" runat="server" colspan="12">
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <tr>
                                                <td align="center" colspan="27">
                                                    <asp:Panel ID="NoDataPane2" runat="server" Visible="false">
                                                        没有数据！</asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="评审结果" TabIndex="2">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 450px; overflow: auto">
                                        <asp:Panel ID="Pan_shenheren" runat="server">
                                            <table width="100%">
                                                <asp:Panel ID="Panel_zd" runat="server">
                                                    <tr>
                                                        <td>
                                                            制单意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_zdanyj" runat="server" Columns="100" Rows="6"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" visible="false">
                                                        <td>
                                                            审核人数:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="tb_pnum" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            制单人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_zdanren" runat="server" CssClass="text1style"
                                                                Enabled="False"></asp:TextBox>
                                                            <asp:TextBox ID="TB_zdanrenid" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_tjiaot"
                                                                runat="server" CssClass="text1style" Enabled="False"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Pan_shenheren1" runat="server" Enabled="false">
                                                    <tr>
                                                        <td>
                                                            采购主管意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj1" runat="server" Columns="100" Rows="3"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="Rad_tongyi1" runat="server" AutoPostBack="true" GroupName="shenhe1"
                                                                OnCheckedChanged="Rad_tongyi1_checkedchanged" Text="同意" TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="Rad_butongyi1" runat="server"
                                                                GroupName="shenhe1" Text="拒绝" TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren1" runat="server" CssClass="text1style"
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="Tb_shenherencode1" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet1"
                                                                runat="server" CssClass="text1style" Enabled="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Pan_shenheren2" runat="server" Enabled="false">
                                                    <tr>
                                                        <td>
                                                            审核意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj2" runat="server" Columns="100" Rows="3"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="Rad_tongyi2" runat="server" AutoPostBack="true" GroupName="shenhe2"
                                                                OnCheckedChanged="Rad_tongyi2_checkedchanged" Text="同意" TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="Rad_butongyi2" runat="server"
                                                                GroupName="shenhe2" Text="拒绝" TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren2" runat="server" CssClass="text1style"
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="Tb_shenherencode2" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet2"
                                                                runat="server" CssClass="text1style" Enabled="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                
                                                <%--审核人3(总经理)--%>
                                                <asp:Panel ID="Pan_shenheren3" runat="server" Enabled="false">
                                                    <tr>
                                                        <td>
                                                            审核意见:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj3" runat="server" Columns="100" Rows="3"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            审核结论:
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="Rad_tongyi3" runat="server" AutoPostBack="true" GroupName="shenhe3"
                                                                OnCheckedChanged="Rad_tongyi3_checkedchanged" Text="同意" TextAlign="Right" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="Rad_butongyi3" runat="server"
                                                                GroupName="shenhe3" Text="拒绝" TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" style="border-bottom: solid 1px black;">
                                                            审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren3" runat="server" CssClass="text1style"
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="Tb_shenherencode3" runat="server" Visible="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet3"
                                                                runat="server" CssClass="text1style" Enabled="false"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </cc1:TabContainer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
