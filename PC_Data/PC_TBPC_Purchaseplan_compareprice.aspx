<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_compareprice.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_compareprice"
    Title="询价单管理" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../PC_Data/PcJs/pricesearch.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <script src="../JS/add.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../SM_Data/javascripts/jquery.js" type="text/javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function openLink(obj) {
            var name;
            name = document.getElementById("<%=TextBoxNO.ClientID%>").value;
            window.showModalDialog('TBPC_Supply.aspx?sheetno=' + name, '', "dialogHeight:400px;dialogWidth:700px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PC_TBPC_Purchaseplan_compareprice.aspx?sheetno=" + name;

        }
    </script>

    <script language="javascript" type="text/javascript">
        function viewCondition() {
            document.getElementById("<%=PanelCondition1.ClientID%>").style.display = 'block';
        }

        function calcu(obj) {
            var a = obj.parentNode.parentNode;
            var con = a.getElementsByTagName('span');
            var b = con[6].innerHTML;
            var c = con[8].innerHTML;
            var con1 = a.getElementsByTagName('input');
            var n = 0;
            for (var i = 0; i < con1.length; i++) {
                if (con1[i].id == obj.id) {
                    n = i;
                }
            }
            var r = document.getElementById('<%=rdobtn.ClientID %>');
            var rs = r.getElementsByTagName('input');
            for (var i = 0; i < rs.length; i++) {
                if (rs[i].checked) {
                    var rval = rs[i].value;
                }
            }
            if (rval == "0") {
                con1[n + 1].value = Math.round(obj.value * b * 100) / 100;
            } else {
                con1[n + 1].value = Math.round(obj.value * b * 100) / 100;
            };
        }

        function changeNum() {
            var nn;
            var r = document.getElementById('<%=rdobtn.ClientID %>');
            var rs = r.getElementsByTagName('input');
            for (var i = 0; i < rs.length; i++) {
                if (rs[i].checked) {
                    var rval = rs[i].value;
                }
            }
            if (rval == "0") {
                nn = 6;
            }
            $.each($(".baseGadget"), function() {
                var a = [11, 14, 17, 20, 23, 26];
                for (var i = 0; i < 6; i++) {
                    var n = a[i];
                    var je = $(this).find('input').eq(n).val();
                    var num = $(this).find('span').eq(nn).html();
                    if (je != '') {
                        $(this).find('input').eq(n + 1).val(Math.round(je * num * 100) / 100);
                    }
                }
            });
        }

        function drapDown(obj, col) {
            var a = $(obj).val();
            $(col).val(a)
        }
    </script>

    <script language="javascript" type="text/javascript">
        //        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        //        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        //        function BeginRequestHandler(sender, args) {

        //        }
        //        function EndRequestHandler(sender, args) {

        //            chk = document.getElementById("ctl00_PrimaryContent_CheckBox8");
        //            if (chk.checked == true) {
        //                var num = "<%=globnum1%>";
        //            }
        //            else {
        //                var num = "<%=globnum1%>";
        //                num = num - 1;
        //            }

        //            var myST = new superTable("tab", {
        //                cssSkin: "tDefault",
        //                headerRows: 2,
        //                fixedCols: num,
        //                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
        //                onStart: function() {
        //                    //		        this.start = new Date();
        //                },
        //                onFinish: function() {
        //                    for (var i = 0, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {

        //                        this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function(i) {
        //                            var clicked = false;

        //                            var dataRow = this.sDataTable.tBodies[0].rows[i];
        //                            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
        //                            return function() {
        //                                if (clicked) {
        //                                    dataRow.style.backgroundColor = "#ffffff";
        //                                    fixedRow.style.backgroundColor = "#ffffff";
        //                                    clicked = false;
        //                                }
        //                                else {
        //                                    dataRow.style.backgroundColor = "#FFC125";
        //                                    fixedRow.style.backgroundColor = "#FFC125";
        //                                    clicked = true;
        //                                }
        //                            }
        //                        } .call(this, i);
        //                    }
        //                    return this;
        //                }
        //            });
        //            $("div.sFData>div.sFDataInner>table input:text").remove();
        //            $("div.sFData>div.sFDataInner>table select").remove();
        //        }
       
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                询比价单
                            </td>
                            <td align="right" width="40%">
                                <input id="Button1" type="button" value="填写供应商" onclick="openLink(this)" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btn_Export" Text="导 出" OnClick="btn_Export_Click" /><asp:HiddenField
                                    ID="HidType" runat="server" Value="0" />
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:FileUpload runat="server" ID="FileUpload1" Width="150px" />
                                <asp:Button runat="server" ID="btn_Import" Text="导 入" OnClick="btn_Import_Click" />
                            </td>
                            <td width="150px" align="right">
                                <asp:CheckBox ID="Chb_Zb" Text="招标物料" runat="server" OnCheckedChanged="zbcheck_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td width="200px" align="right">
                                <asp:RadioButtonList runat="server" ID="rdobtn" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" onclick="changeNum()">采购数目</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_autobj" runat="server" Text="比价保存" OnClick="btn_autobj_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_BZJ" runat="server" Text="比总价保存" OnClick="btn_BZJ_Click" Visible="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="保存" OnClick="btn_confirm_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_change" runat="server" Text="变更" OnClick="btn_change_Click" Visible="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;
                                <asp:Label ID="Labelerror" runat="server" Text="" Visible="False" ForeColor="Red"></asp:Label>
                                <asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                    <asp:Image ID="Img_print" runat="server" ImageUrl="~/Assets/icon-fuction/89.gif"
                                        title="打印" /></asp:HyperLink>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="60%" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="60%" style="background-color: #CCCCFF; border: solid 1px black;" border="1">
                                    <tr>
                                        <td colspan="4" align="center">
                                            选择要隐藏的列
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
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox13" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="辅助数量" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox14" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Text="辅助单位" TextAlign="Right"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Panel ID="PanelCondition1" runat="server" Width="60%" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table id="tab1" class="nowrap cptable fullwidth" width="100%">
                                    <tr>
                                        <td>
                                            供应商1
                                        </td>
                                        <td>
                                            供应商2
                                        </td>
                                        <td>
                                            供应商3
                                        </td>
                                        <td>
                                            供应商4
                                        </td>
                                        <td>
                                            供应商5
                                        </td>
                                        <td>
                                            供应商6
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="PIC_SUPPLIERANAME" runat="server" Text="" onblur="providerchanged(0)"></asp:TextBox>
                                            <asp:Label ID="LbA_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PIC_SUPPLIERAID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PIC_SUPPLIERA" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PIC_SUPPLIERBNAME" runat="server" Text="" onblur="providerchanged(1)"></asp:TextBox>
                                            <asp:Label ID="LbB_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PIC_SUPPLIERBID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PIC_SUPPLIERB" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PIC_SUPPLIERCNAME" runat="server" Text="" onblur="providerchanged(2)"></asp:TextBox>
                                            <asp:Label ID="LbC_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PIC_SUPPLIERCID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PIC_SUPPLIERC" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PIC_SUPPLIERDNAME" runat="server" Text="" onblur="providerchanged(3)"></asp:TextBox>
                                            <asp:Label ID="LbD_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PIC_SUPPLIERDID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PIC_SUPPLIERD" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PIC_SUPPLIERENAME" runat="server" Text="" onblur="providerchanged(4)"></asp:TextBox>
                                            <asp:Label ID="LbE_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PIC_SUPPLIEREID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PIC_SUPPLIERE" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PIC_SUPPLIERFNAME" runat="server" Text="" onblur="providerchanged(5)"></asp:TextBox>
                                            <asp:Label ID="LbF_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PIC_SUPPLIERFID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PIC_SUPPLIERF" runat="server" Text="" Style="display: none"></asp:TextBox>
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
                                <td>
                                    单据编号：<asp:TextBox ID="TextBoxNO" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:TextBox ID="Tb_state" runat="server" Text="" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    日期:
                                    <asp:TextBox ID="tb_time" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr align="right">
                                <td align="right">
                                    排序方式：
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rad_Paixu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rad_Paixu_Changed"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="计划跟踪号" Selected="True" Value="ptcode"></asp:ListItem>
                                        <asp:ListItem Text="物料编码" Value="marid"></asp:ListItem>
                                        <asp:ListItem Text="名称" Value="marnm"></asp:ListItem>
                                        <asp:ListItem Text="规格" Value="margg"></asp:ListItem>
                                        <asp:ListItem Text="采购数量" Value="marzxnum"></asp:ListItem>
                                        <asp:ListItem Text="计划跟踪号+物料代码" Value="ptcode,marid"></asp:ListItem>
                                        <asp:ListItem Text="名称+规格" Value="marnm,margg"></asp:ListItem>
                                        <asp:ListItem Text="名称+采购数量" Value="marnm,marzxnum"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="询比价单">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 600px">
                                        <div class="cpbox8 xscroll">
                                            <table id="tab" class=" cptable fullwidth" width="100%">
                                                <asp:Repeater ID="comparepriceRepeater" runat="server" OnItemDataBound="comparepriceRepeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td id="wlxx" runat="server" colspan="22" rowspan="1">
                                                                物料信息
                                                            </td>
                                                            <td colspan="1" rowspan="1">
                                                                比价结果
                                                            </td>
                                                            <td id="gys1" runat="server" colspan="3" rowspan="1">
                                                                供应商1
                                                                <asp:Label ID="GYShang1" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys2" runat="server" colspan="3" rowspan="1">
                                                                供应商2
                                                                <asp:Label ID="GYShang2" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys3" runat="server" colspan="3" rowspan="1">
                                                                供应商3
                                                                <asp:Label ID="GYShang3" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys4" runat="server" colspan="3" rowspan="1">
                                                                供应商4
                                                                <asp:Label ID="GYShang4" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys5" runat="server" colspan="3" rowspan="1">
                                                                供应商5
                                                                <asp:Label ID="GYShang5" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys6" runat="server" colspan="3" rowspan="1">
                                                                供应商6
                                                                <asp:Label ID="GYShang6" runat="server"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td id="jhhao" runat="server">
                                                                <strong>计划跟踪号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>部件名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>部件图号</strong>
                                                            </td>
                                                            <td id="marguobiao" runat="server">
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td id="martuhao" runat="server">
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td id="marmingcheng" runat="server">
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td id="marguige" runat="server">
                                                                <strong>规格</strong>
                                                            </td>
                                                            <td id="marcaizhi" runat="server">
                                                                <strong>材质</strong>
                                                            </td>
                                                            <td id="length" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td id="width" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td id="danwei" runat="server">
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td id="cgshuliang" runat="server">
                                                                <strong>采购数量</strong>
                                                            </td>
                                                            <td id="fzunit1" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td id="fznum1" runat="server" visible="false">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td id="zxfznum1" runat="server">
                                                                <strong>执行辅助数量</strong>
                                                            </td>
                                                            <td id="beizhu" runat="server">
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td id="mashape" runat="server">
                                                                <strong>类型</strong>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <strong>计划模式</strong>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <strong>是否关键部件</strong>
                                                            </td>
                                                            <td>
                                                                <strong>申请人</strong>
                                                            </td>
                                                            <td id="marbianma" runat="server">
                                                                <strong>物料编码</strong>
                                                            </td>
                                                            <td id="jine" runat="server">
                                                                <strong>金额</strong>
                                                            </td>
                                                            <td id="shuilv" runat="server">
                                                                <strong>税率(%)</strong>
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
                                                            <td id="dsc1" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc2" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec2" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="dsc2" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc3" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec3" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="dsc3" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc4" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec4" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="dsc4" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc5" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec5" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="dsc5" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td id="dyc6" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dec6" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td id="dsc6" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <FooterTemplate>
                                                        <tr align="center">
                                                            <td id="foot1" runat="server" colspan="23" rowspan="1">
                                                                汇总(RMB/元)
                                                            </td>
                                                            <td id="zjg12" runat="server" align="right" colspan="2" width="90px">
                                                            </td>
                                                            <td id="zjg13" runat="server" width="90px">
                                                                <asp:TextBox ID="totalTextBox13" runat="server" BorderStyle="None" Enabled="False"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td id="zjg22" runat="server" align="right" colspan="2" width="90px">
                                                            </td>
                                                            <td id="zjg23" runat="server" width="90px">
                                                                <asp:TextBox ID="totalTextBox23" runat="server" BorderStyle="None" Enabled="False"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td id="zjg32" runat="server" align="right" colspan="2" width="90px">
                                                            </td>
                                                            <td id="zjg33" runat="server" width="90px">
                                                                <asp:TextBox ID="totalTextBox33" runat="server" BorderStyle="None" Enabled="False"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td id="zjg42" runat="server" align="right" colspan="2" width="90px">
                                                            </td>
                                                            <td id="zjg43" runat="server" width="90px">
                                                                <asp:TextBox ID="totalTextBox43" runat="server" BorderStyle="None" Enabled="False"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td id="zjg52" runat="server" align="right" colspan="2" width="90px">
                                                            </td>
                                                            <td id="zjg53" runat="server" width="90px">
                                                                <asp:TextBox ID="totalTextBox53" runat="server" BorderStyle="None" Enabled="False"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td id="zjg62" runat="server" align="right" colspan="2" width="90px">
                                                            </td>
                                                            <td id="zjg63" runat="server" width="90px">
                                                                <asp:TextBox ID="totalTextBox63" runat="server" BorderStyle="None" Enabled="False"
                                                                    Width="90px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                                            <td>
                                                                <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" Checked="false"
                                                                    CssClass="checkBoxCss" onclick="checkme(this)" />
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" Text="<%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*50%>"></asp:Label>
                                                            </td>
                                                            <td id="jhhao1" runat="server">
                                                                <asp:Label ID="PIC_PCODE" runat="server" Text='<%#Eval("ptcode")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_ptcode" runat="server" Text='<%#Eval("ptcode")%>' BorderStyle="None"
                                                                    Width="150px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("ptcode")%>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_CHILDENGNAME" runat="server" Text='<%#Eval("PIC_CHILDENGNAME")%>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_CHILDENGNAME" runat="server" Text='<%#Eval("PIC_CHILDENGNAME")%>'
                                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                                    ToolTip='<%#Eval("PIC_CHILDENGNAME")%>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MAP" runat="server" Text='<%#Eval("PIC_MAP")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_MAP" runat="server" Text='<%#Eval("PIC_MAP")%>' BorderStyle="None"
                                                                    Width="80px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("PIC_MAP")%>'></asp:TextBox>
                                                            </td>
                                                            <td id="marguobiao1" runat="server">
                                                                <asp:Label ID="PIC_GUOBIAO" runat="server" Text='<%#Eval("margb")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_GUOBIAO" runat="server" Text='<%#Eval("margb")%>' BorderStyle="None"
                                                                    Width="80px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("margb")%>'></asp:TextBox>
                                                            </td>
                                                            <td id="martuhao1" runat="server">
                                                                <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>' BorderStyle="None"
                                                                    Width="80px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("PIC_TUHAO")%>'></asp:TextBox>
                                                            </td>
                                                            <td id="marmingchen1" runat="server">
                                                                <asp:Label ID="PIC_MARNAME" runat="server" Text='<%#Eval("marnm")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_MARNAME" runat="server" Text='<%#Eval("marnm")%>' BorderStyle="None"
                                                                    Width="100px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("marnm")%>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MARGUIGE" runat="server" Text='<%#Eval("margg")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_MARGUIGE" runat="server" Text='<%#Eval("margg")%>' BorderStyle="None"
                                                                    Width="80px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("margg")%>'></asp:TextBox>
                                                            </td>
                                                            <td id="marcaizhi1" runat="server">
                                                                <asp:Label ID="PIC_MARCAIZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                            </td>
                                                            <td id="length1" runat="server">
                                                                <asp:Label ID="PIC_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                            </td>
                                                            <td id="width1" runat="server">
                                                                <asp:Label ID="PIC_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <asp:Label ID="PIC_QUANTITY" runat="server" Text='<%#Eval("marnum")%>'></asp:Label>
                                                            </td>
                                                            <td id="danwwei1" runat="server">
                                                                <asp:Label ID="PIC_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                            </td>
                                                            <td id="cgshuliang1" runat="server">
                                                                <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                            </td>
                                                            <td id="fzunit2" runat="server">
                                                                <asp:Label ID="PIC_FZUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                            </td>
                                                            <td id="fznum2" runat="server" visible="false">
                                                                <asp:Label ID="PIC_FZNUM" runat="server" Text='<%#Eval("marfznum")%>'></asp:Label>
                                                            </td>
                                                            <td id="zxfznum2" runat="server">
                                                                <asp:Label ID="PIC_ZXFUNUM" runat="server" Text='<%#Eval("marzxfznum")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="PIC_ZXFUNUM" runat="server" Text='<%#Eval("marzxfznum")%>' Width="60px" Enabled="false"></asp:TextBox>--%>
                                                            </td>
                                                            <td id="beizhu1" runat="server">
                                                                <%--<asp:TextBox ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>' BorderStyle="None"></asp:TextBox>--%>
                                                                <asp:Label ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>' Visible="false"></asp:Label>
                                                                <asp:TextBox ID="txt_NOTE" runat="server" Text='<%#Eval("detailnote")%>' BorderStyle="None"
                                                                    Width="120px" Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("detailnote")%>'></asp:TextBox>
                                                                <%--<input id="PIC_NOTE" type="text"  runat="server" value='<%#Eval("detailnote")%>' />--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="mashape1" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <asp:Label ID="PIC_PMODE" runat="server" Text='<%#Eval("pmode")%>'></asp:Label>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <asp:Label ID="PIC_KEYCOMS" runat="server" Text='<%#Eval("keycoms")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="ST_SQR" runat="server" Text='<%#Eval("ST_SQR")%>'></asp:Label>
                                                            </td>
                                                            <td id="marbianma1" runat="server">
                                                                <asp:Label ID="PIC_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                            </td>
                                                            <td id="jine1" runat="server">
                                                                <asp:Label ID="Amount" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                            </td>
                                                            <td id="shuilv1" runat="server">
                                                                <asp:TextBox ID="PIC_SHUILV" runat="server" Text='<%#Eval("shuilv")%>' Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drp_supplier" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="PIC_SUPPLIERRESID" runat="server" BorderStyle="None" Text='<%#Eval("supplierresid")%>'
                                                                    Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td id="jg11" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox11" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    class="rq1" Text='<%#Eval("qoutefstsa")%>' Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd"
                                                                    TargetControlID="TextBox11">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td id="jg12" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox12" runat="server" BorderStyle="None" onblur="calcu(this)"
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" Text='<%#Eval("qoutescdsa")%>'
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox12"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg13" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox13" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("qoutelstsa")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox13"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg21" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox21" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    class="rq2" Text='<%#Eval("qoutefstsb")%>' Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd"
                                                                    TargetControlID="TextBox21">
                                                                </cc1:CalendarExtender>       
                                                            </td>
                                                            <td id="jg22" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox22" runat="server" BorderStyle="None" onblur="calcu(this)"
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" Text='<%#Eval("qoutescdsb")%>'
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox22"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg23" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox23" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("qoutelstsb")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox23"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg31" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox31" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    class="rq3" Text='<%#Eval("qoutefstsc")%>' Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd"
                                                                    TargetControlID="TextBox31">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td id="jg32" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox32" runat="server" BorderStyle="None" onblur="calcu(this)"
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" Text='<%#Eval("qoutescdsc")%>'
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextBox32"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg33" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox33" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("qoutelstsc")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TextBox33"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg41" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox41" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    class="rq4" Text='<%#Eval("qoutefstsd")%>' Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd"
                                                                    TargetControlID="TextBox41">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td id="jg42" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox42" runat="server" BorderStyle="None" onblur="calcu(this)"
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" Text='<%#Eval("qoutescdsd")%>'
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                    ControlToValidate="TextBox42" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg43" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox43" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("qoutelstsd")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                    ControlToValidate="TextBox43" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg51" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox51" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    class="rq5" Text='<%#Eval("qoutefstse")%>' Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="yyyy-MM-dd"
                                                                    TargetControlID="TextBox51">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td id="jg52" runat="server" width="110px">
                                                                <asp:TextBox ID="TextBox52" runat="server" BorderStyle="None" onblur="calcu(this)"
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" Text='<%#Eval("qoutescdse")%>'
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                                    ControlToValidate="TextBox52" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg53" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox53" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("qoutelstse")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                                    ControlToValidate="TextBox53" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg61" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox61" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    class="rq6" Text='<%#Eval("qoutefstsf")%>' Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="yyyy-MM-dd"
                                                                    TargetControlID="TextBox61">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td id="jg62" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox62" runat="server" BorderStyle="None" onblur="calcu(this)"
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" Text='<%#Eval("qoutescdsf")%>'
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                                    ControlToValidate="TextBox62" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td id="jg63" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox63" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("qoutelstsf")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                                                    ControlToValidate="TextBox63" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="41">
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="False">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="height: 15px">
                                            <table align="center" border="1" class="nowrap cptable fullwidth" width="100%">
                                                <tr>
                                                    <td colspan="6">
                                                        共<asp:Label ID="lb_count" runat="server" Text="Label"></asp:Label>
                                                        条记录&nbsp; 共<asp:Label ID="lb_page" runat="server" Text="Label"></asp:Label>
                                                        页 &nbsp; 当前第<asp:Label ID="lb_CurrentPage" runat="server" Text="1"></asp:Label>
                                                        页&nbsp;
                                                        <asp:LinkButton ID="LinkFirst" runat="server" OnClick="LinkFirst_Click">第一页</asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="LinkUp" runat="server" OnClick="LinkUp_Click">上一页</asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="LinkDown" runat="server" OnClick="LinkDown_Click">下一页</asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="LinkLast" runat="server" OnClick="LinkLast_Click">最后一页</asp:LinkButton>
                                                        &nbsp; 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        页 &nbsp;&nbsp;&nbsp;&nbsp; 每页显示<asp:DropDownList runat="server" ID="DDLPage" AutoPostBack="true"
                                                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr visible="false" runat="server">
                                                    <td>
                                                        制&nbsp;&nbsp;单&nbsp;&nbsp;人:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_zd" runat="server" Enabled="False"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        部&nbsp;门&nbsp;负&nbsp;责&nbsp;人:
                                                    </td>
                                                    <td>
                                                        <asp:ComboBox ID="cob_fuziren" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                                                            Height="15px" MaxLength="0" Width="120px">
                                                        </asp:ComboBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        主&nbsp;管&nbsp;经&nbsp;理:
                                                    </td>
                                                    <td>
                                                        <asp:ComboBox ID="cob_zgjl" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                                                            Height="15px" MaxLength="0" Width="120px">
                                                        </asp:ComboBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </div>

                                        <script language="javascript" type="text/javascript">

                                            //                                            chk = document.getElementById("ctl00_PrimaryContent_CheckBox8");
                                            //                                            if (chk.checked == true) {
                                            //                                                var num = "<%=globnum1%>";
                                            //                                            }
                                            //                                            else {
                                            //                                                var num = "<%=globnum1%>";
                                            //                                                num = num - 1;
                                            //                                            }
                                            //                                            //        var num="<%=globnum1%>"; 
                                            //                                            var myST = new superTable("tab", {
                                            //                                                cssSkin: "tDefault",
                                            //                                                headerRows: 2,
                                            //                                                fixedCols: num,
                                            //                                                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                            //                                                onStart: function() {
                                            //                                                    //		        this.start = new Date();
                                            //                                                },
                                            //                                                onFinish: function() {
                                            //                                                    for (var i = 0, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {

                                            //                                                        this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function(i) {
                                            //                                                            var clicked = false;

                                            //                                                            var dataRow = this.sDataTable.tBodies[0].rows[i];
                                            //                                                            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                                            //                                                            return function() {
                                            //                                                                if (clicked) {
                                            //                                                                    dataRow.style.backgroundColor = "#ffffff";
                                            //                                                                    fixedRow.style.backgroundColor = "#ffffff";
                                            //                                                                    clicked = false;
                                            //                                                                }
                                            //                                                                else {
                                            //                                                                    dataRow.style.backgroundColor = "#FFC125";
                                            //                                                                    fixedRow.style.backgroundColor = "#FFC125";
                                            //                                                                    clicked = true;
                                            //                                                                }
                                            //                                                            }
                                            //                                                        } .call(this, i);
                                            //                                                    }
                                            //                                                    return this;
                                            //                                                }
                                            //                                            });
                                            //                                            $("div.sFData>div.sFDataInner>table input:text").remove();
                                            //                                            $("div.sFData>div.sFDataInner>table select").remove();
                                        </script>

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
