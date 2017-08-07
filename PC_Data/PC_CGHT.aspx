<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PC_CGHT.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购合同 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 80%;
            border: solid 1px #E5E5E5;
        }
        .tab tr
        {
            height: 30px;
        }
        .tab tr td
        {
            border: solid 1px #E5E5E5;
            border-collapse: collapse;
        }
    </style>
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
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            var inputs = document.getElementById('<%=TabPanel0.ClientID%>').getElementsByTagName("input");
            var a = 0;
            for (var i = 0, length = inputs.length; i < length; i++) {
                if (inputs[i].type == "text" && inputs[i].id != "ctl00_PrimaryContent_TabContainer0_TabPanel0_txt_HT_QDSJ") {
                    inputs[i].style.height = "96%";
                    inputs[i].style.width = "90%";
                }
                if (inputs[i].type == "text" && inputs[i].id == "ctl00_PrimaryContent_TabContainer0_TabPanel0_txt_HT_QDSJ") {
                    a = i + 1;
                }
            }
            inputs[a].style.height = "96%";
            inputs[a].style.width = "80%";
            inputs[a].readOnly = true;
            //            alert(inputs[10].id);
        })

        function dai() {
            //            alert('1')
            var provider = $("#<%=txt_HT_GF.ClientID%>").val();
            //            alert(provider);
            if (provider != "") {
                if (provider.indexOf('|') > 0) {
                    var str = provider.split('|');
                    //                    alert(str);
                    $("#<%=txt_HT_GF.ClientID%>").val(str[0]);
                    $("#<%=txt_HT_GF1.ClientID%>").val(str[0]);
                    $("#<%=txt_HT_DZ.ClientID%>").val(str[1]);
                    $("#<%=txt_HT_DH.ClientID%>").val(str[2]);
                    $("#<%=txt_HT_CZ.ClientID%>").val(str[3]);
                    $("#<%=txt_HT_KHYH.ClientID%>").val(str[4]);
                    $("#<%=txt_HT_ZH.ClientID%>").val(str[5]);
                    $("#<%=txt_HT_SH.ClientID%>").val(str[6]);
                    $("#<%=txt_HT_YB.ClientID%>").val(str[7]);
                }
                else {
                    $("#<%=txt_HT_GF1.ClientID%>").val(str[0]);
                }
            }
        }

        function beizhu() {
            $.each($("#<%=rbl_HT_JSFS.ClientID%> input"), function() {
                if (this.checked && this.value == "1") {
                    $("#<%=txt_HT_JSFSBZ.ClientID%>").val("货到付款型——货到验收合格并卖方开具全额有效的增值税后，买方支付合同货款。");
                }
                if (this.checked && this.value == "2") { $("#<%=txt_HT_JSFSBZ.ClientID%>").val("款到发货型——卖方通知买方合同约定的所有物资具备发货条件，买方付清全部货款，卖方发货；货到验收合格后，卖方30天内开具全额有效的增值税发票。"); }
            })
        }

        function fkfs2(obj) {
            alert(obj);
            alert(obj.checked);
            if (obj.checked) {
                $("#<%=txt_HT_JSFS1BZ.ClientID%>").val("本合同约定的所有货物到货，卖方提交全部交货资料并开具全额有效的增值税发票后30天内，买方支付合同总价款的95%发货款。");
            }
            else {
                $("#<%=txt_HT_JSFS1BZ.ClientID%>").val("");
            }
        }

        function fkfs3(obj) {
            if (obj.checked) {
                $("#<%=txt_HT_JSFS2BZ.ClientID%>").val("合同总价款的5％作为产品的质保金，待质保期满后一个月内〔扣除因卖方责任而承担的费用〕付清（质保期12个月）。");
            }
            else {
                $("#<%=txt_HT_JSFS2BZ.ClientID%>").val("");
            }
        }

        function DaXie(obj) {
            var money = $("#<%=txt_HT_HTZJ.ClientID%>").val();
            var n = $("#<%=txt_HT_HTZJ.ClientID%>").val();
            if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n))
                return "数据非法";
            var unit = "千百拾亿千百拾万千百拾元角分", str = "";
            n += "00";
            var p = n.indexOf('.');
            if (p >= 0)
                n = n.substring(0, p) + n.substr(p + 1, 2);
            unit = unit.substr(unit.length - n.length);
            for (var i = 0; i < n.length; i++)
                str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);
            var a = str.replace(/零(千|百|拾|角)/g, "零").replace(/(零)+/g, "零").replace(/零(万|亿|元)/g, "$1").replace(/(亿)万|壹(拾)/g, "$1$2").replace(/^元零?|零分/g, "").replace(/元$/g, "元整");
            $("#<%=txt_HT_HTZJ.ClientID%>").val(a + "(" + money + ")");
        }


        //添加收款
        function btnADDCR_onclick() {
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog('PC_CGHT_Payment.aspx?Action=Add&condetail_id=' + $("#<%=txt_HT_XFHTBH.ClientID %>").val() + '&NoUse=' + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
        }
        //修改要款
        function BPEditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("PC_CGHT_Payment.aspx?Action=Edit&BPid=" + ID + "&NoUse=" + time, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
        }


        //添加发票
        function btnAddFP_onclick() {
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog('PC_CGHT_Bill.aspx?Action=Add&condetail_id=' + $("#<%=txt_HT_XFHTBH.ClientID %>").val() + '&NoUse=' + time, obj, "dialogWidth=720px;dialogHeight=350px;status:no;");
            if (sRet == "refresh") {
                window.history.go(0);
            }
        }
        //修改发票
        function BREditDetail(i) {
            var ID = i.title;
            var date = new Date();
            var time = date.getTime();
            var obj = new Object();

            var sRet = window.showModalDialog("PC_CGHT_Bill.aspx?Action=Edit&BRid=" + ID + "&NoUse=" + time, obj, "dialogWidth=620px;dialogHeight=450px;status:no;");
            if (sRet == "refresh") {
                //window.history.go(0); 
            }
        }


        //查看发票
        function BRViewDetail(i) {
            var ID = i.title;
            var obj = new Object();

            window.showModalDialog("PC_CGHT_Bill.aspx?Action=View&BRid=" + ID, obj, "dialogWidth=620px;dialogHeight=450px;status:no;");
        }

        function add_bcxy_onclick() {
            var hth = $("#<%=txt_HT_XFHTBH.ClientID %>").val();
            location.href = "PC_CGHT.aspx?action=bcxy&hth=" + hth;
        }
    </script>

    <script type="text/javascript">
        //选择审批人
        $(function() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=tb1.ClientID%>").show();
                    $("#<%=tb2.ClientID%>").hide();
                    $("#<%=tb3.ClientID%>").hide();
                    $("#<%=tb4.ClientID%>").hide();
                    $("#<%=tb5.ClientID%>").hide();
                    $("#<%=tb6.ClientID%>").hide();
                    $("#<%=tb7.ClientID%>").hide();
                    $("#<%=tb8.ClientID%>").hide();
                    $("#<%=tb9.ClientID%>").hide();
                    $("#<%=tb10.ClientID%>").hide();
                    if ($("#<%=txt_HT_SHR1.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR1.ClientID%>").val("高浩");
                    }
                }
                if ($(this).val() == "2") {
                    $("#<%=tb1.ClientID%>").show();
                    $("#<%=tb2.ClientID%>").show();
                    $("#<%=tb3.ClientID%>").hide();
                    $("#<%=tb4.ClientID%>").hide();
                    $("#<%=tb5.ClientID%>").hide();
                    $("#<%=tb6.ClientID%>").hide();
                    $("#<%=tb7.ClientID%>").hide();
                    $("#<%=tb8.ClientID%>").hide();
                    $("#<%=tb9.ClientID%>").hide();
                    $("#<%=tb10.ClientID%>").hide();
                    if ($("#<%=txt_HT_SHR1.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR1.ClientID%>").val("高浩");
                    }
                    if ($("#<%=txt_HT_SHR2.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR2.ClientID%>").val("王福泉");
                    }
                }
                if ($(this).val() == "3") {
                    $("#<%=tb1.ClientID%>").show();
                    $("#<%=tb2.ClientID%>").show();
                    $("#<%=tb3.ClientID%>").show();
                    $("#<%=tb4.ClientID%>").hide();
                    $("#<%=tb5.ClientID%>").hide();
                    $("#<%=tb6.ClientID%>").hide();
                    $("#<%=tb7.ClientID%>").hide();
                    $("#<%=tb8.ClientID%>").hide();
                    $("#<%=tb9.ClientID%>").hide();
                    $("#<%=tb10.ClientID%>").hide();
                    if ($("#<%=txt_HT_SHR1.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR1.ClientID%>").val("高浩");
                    }
                    if ($("#<%=txt_HT_SHR2.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR2.ClientID%>").val("王福泉");
                    }
                    if ($("#<%=txt_HT_SHR3.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR3.ClientID%>").val("王自清");
                    }
                }
                if ($(this).val() == "4") {
                    $("#<%=tb1.ClientID%>").hide();
                    $("#<%=tb2.ClientID%>").hide();
                    $("#<%=tb3.ClientID%>").hide();
                    $("#<%=tb4.ClientID%>").show();
                    $("#<%=tb5.ClientID%>").show();
                    $("#<%=tb6.ClientID%>").show();
                    $("#<%=tb7.ClientID%>").show();
                    $("#<%=tb8.ClientID%>").show();
                    $("#<%=tb9.ClientID%>").show();
                    $("#<%=tb10.ClientID%>").show();
//                    $("#<%=tb10.ClientID%>").hide();
                    if ($("#<%=txt_HT_SHRCG.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRCG.ClientID%>").val("高浩");
                    }
                    if ($("#<%=txt_HT_SHRShenC.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRShenC.ClientID%>").val("于来义");
                    }
                    if ($("#<%=txt_HT_SHRJS.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRJS.ClientID%>").val("李小婷");
                    }
                    //质量
                    if ($("#<%=txt_HT_SHRZ.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRZ.ClientID%>").val("陈永秀");
                    }
                    if ($("#<%=txt_HT_SHRShiC.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRShiC.ClientID%>").val("李利恒");
                    }
                    if ($("#<%=txt_HT_SHRCW.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRCW.ClientID%>").val("叶宝松");
                    }
                    if ($("#<%=txt_HT_SHRFZ.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRFZ.ClientID%>").val("王福泉");
                    }
                }
            });
        })

        function rbl_HT_SPLX_rbl_HT_SPLX() {

            //审批*********
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    fz();
                    $("#<%=tb1.ClientID%>").show();
                    if ($("#<%=txt_HT_SHR1.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR1.ClientID%>").val("高浩");
                    }
                }
                if ($(this).val() == "2") {
                    fz();
                    $("#<%=tb1.ClientID%>").show();
                    $("#<%=tb2.ClientID%>").show();
                    if ($("#<%=txt_HT_SHR1.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR1.ClientID%>").val("高浩");
                    }
                    if ($("#<%=txt_HT_SHR2.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR2.ClientID%>").val("王福泉");
                    }
                }
                if ($(this).val() == "3") {
                    fz();
                    $("#<%=tb1.ClientID%>").show();
                    $("#<%=tb2.ClientID%>").show();
                    $("#<%=tb3.ClientID%>").show();
                    if ($("#<%=txt_HT_SHR1.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR1.ClientID%>").val("高浩");
                    }
                    if ($("#<%=txt_HT_SHR2.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR2.ClientID%>").val("王福泉");
                    }
                    if ($("#<%=txt_HT_SHR3.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHR3.ClientID%>").val("王自清");
                    }
                }
                if ($(this).val() == "4") {
                    fz();
                    $("#<%=tb4.ClientID%>").show();
                    $("#<%=tb5.ClientID%>").show();
                    $("#<%=tb6.ClientID%>").show();
                    $("#<%=tb7.ClientID%>").show();
                    $("#<%=tb8.ClientID%>").show();
                    $("#<%=tb9.ClientID%>").show();
                    $("#<%=tb10.ClientID%>").show();
                    //$("#<%=tb10.ClientID%>").hide();
                    if ($("#<%=txt_HT_SHRCG.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRCG.ClientID%>").val("高浩");
                    }
                    if ($("#<%=txt_HT_SHRShenC.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRShenC.ClientID%>").val("于来义");
                    }
                    if ($("#<%=txt_HT_SHRJS.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRJS.ClientID%>").val("李小婷");
                    }
                    //质量
                    if ($("#<%=txt_HT_SHRZ.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRZ.ClientID%>").val("陈永秀");
                    }
                    if ($("#<%=txt_HT_SHRShiC.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRShiC.ClientID%>").val("李利恒");
                    }
                    if ($("#<%=txt_HT_SHRCW.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRCW.ClientID%>").val("叶宝松");
                    }
                    if ($("#<%=txt_HT_SHRFZ.ClientID%>").val() == "") {
                        $("#<%=txt_HT_SHRFZ.ClientID%>").val("王福泉");
                    }
                }
            });
        }

        function fz() {
            $("#<%=tb1.ClientID%>").hide();
            $("#<%=tb2.ClientID%>").hide();
            $("#<%=tb3.ClientID%>").hide();
            $("#<%=tb4.ClientID%>").hide();
            $("#<%=tb5.ClientID%>").hide();
            $("#<%=tb6.ClientID%>").hide();
            $("#<%=tb7.ClientID%>").hide();
            $("#<%=tb8.ClientID%>").hide();
            $("#<%=tb9.ClientID%>").hide();
            $("#<%=tb10.ClientID%>").hide();//质量

            $("#<%=txt_HT_SHR1.ClientID%>").val("");
            $("#<%=txt_HT_SHR2.ClientID%>").val("");
            $("#<%=txt_HT_SHR3.ClientID%>").val("");
            $("#<%=txt_HT_SHRCG.ClientID%>").val("");
            $("#<%=txt_HT_SHRShenC.ClientID%>").val("");
            $("#<%=txt_HT_SHRJS.ClientID%>").val("");
            $("#<%=txt_HT_SHRShiC.ClientID%>").val("");
            $("#<%=txt_HT_SHRCW.ClientID%>").val("");
            $("#<%=txt_HT_SHRFZ.ClientID%>").val("");
            $("#<%=txt_HT_SHRZ.ClientID%>").val("");//质量
        }

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        function xr3() {
            $("#hidPerson").val("person3");
            SelPersons();
        }
        function xr4() {
            $("#hidPerson").val("person4");
            SelPersons();
        }
        function xr5() {
            $("#hidPerson").val("person5");
            SelPersons();
        }
        function xr6() {
            $("#hidPerson").val("person6");
            SelPersons();
        }
        function xr7() {
            $("#hidPerson").val("person7");
            SelPersons();
        }
        function xr8() {
            $("#hidPerson").val("person8");
            SelPersons();
        }
        function xr9() {
            $("#hidPerson").val("person9");
            SelPersons();
        }
        //质量
        function xr10() {
            $("#hidPerson").val("person10");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txt_HT_SHR1.ClientID %>").val(r.st_name);
            }
            if (id == "person2") {
                $("#<%=txt_HT_SHR2.ClientID %>").val(r.st_name);
            }
            if (id == "person3") {
                $("#<%=txt_HT_SHR3.ClientID %>").val(r.st_name);
            }
            if (id == "person4") {
                $("#<%=txt_HT_SHRCG.ClientID %>").val(r.st_name);
            }
            if (id == "person5") {
                $("#<%=txt_HT_SHRShenC.ClientID %>").val(r.st_name);
            }
            if (id == "person6") {
                $("#<%=txt_HT_SHRJS.ClientID %>").val(r.st_name);
            }
            if (id == "person7") {
                $("#<%=txt_HT_SHRShiC.ClientID %>").val(r.st_name);
            }
            if (id == "person8") {
                $("#<%=txt_HT_SHRCW.ClientID %>").val(r.st_name);
            }
            if (id == "person9") {
                $("#<%=txt_HT_SHRFZ.ClientID %>").val(r.st_name);
            }
            //质量
            if (id == "person10") {
                $("#<%=txt_HT_SHRZ.ClientID %>").val(r.st_name);
            }
            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div style="width: 100%;" align="right">
        <asp:Button runat="server" ID="btnSubmit" Text="提 交" OnClick="btnSubmit_OnClick" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnBack" Text="返 回" OnClick="btnBack_OnClick" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <asp:TabContainer runat="server" ID="TabContainer0" ActiveTabIndex="1">
        <asp:TabPanel runat="server" ID="TabPanel0" HeaderText="买卖合同">
            <ContentTemplate>
                <div id="div2" style="width: 100%;" align="center">
                    <asp:Panel runat="server" ID="Panel1">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="font-size: xx-large;">
                                    <strong>买&nbsp;&nbsp;卖&nbsp;&nbsp;合&nbsp;&nbsp;同</strong>
                                    <asp:HiddenField ID="hidZY" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table class="tab">
                            <tr>
                                <td style="width: 20%">
                                    买方（需方）：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label runat="server" ID="lb_XF" Text="中材（天津）重型机械有限公司"></asp:Label>
                                </td>
                                <td style="width: 20%">
                                    卖方（供方）：
                                </td>
                                <td style="width: 30%">
                                    <asp:TextBox runat="server" ID="txt_HT_GF" onchange="dai()"></asp:TextBox>
                                    <asp:AutoCompleteExtender runat="server" ID="aceGYS" TargetControlID="txt_HT_GF"
                                        ServicePath="PC_ZDPP.asmx" MinimumPrefixLength="1" CompletionInterval="10"
                                        ServiceMethod="GetGYS_NAME" FirstRowSelected="True" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                        DelimiterCharacters="" Enabled="True">
                                    </asp:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    买方合同号编号：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_XFHTBH" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td>
                                    卖方合同编号：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_GFHTBH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    签订时间：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_QDSJ" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>
                                </td>
                                <td>
                                    市场部合同号：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_SCBHTBH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: large">
                                    <strong>合同总价：</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_HTZJ" runat="server" onchange="DaXie(this)"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: large" colspan="4">
                                    <strong>结算方式及期限：</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    1、 
                                </td>
                                <td align="center" colspan="3">
                                    <asp:RadioButtonList ID="rbl_HT_JSFS" runat="server" onchange="beizhu()" 
                                        RepeatDirection="Horizontal" Width="60%">
                                        <asp:ListItem Text="货到付款型" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="款到发货型" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left">
                                    备注：<asp:TextBox ID="txt_HT_JSFSBZ" runat="server" Rows="2" TextMode="MultiLine" 
                                        Width="96%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    2、<asp:CheckBox ID="cbxHT_JSFS1" runat="server" onclick="fkfs2(this)" 
                                        Text="发货款" />
                                    <asp:TextBox ID="txt_HT_JSFS1BZ" runat="server" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left">
                                    3、<asp:CheckBox ID="cbxHT_JSFS2" runat="server" onclick="fkfs3(this)" 
                                        Text="质保金" />
                                    <asp:TextBox runat="server" ID="txt_HT_JSFS2BZ"
                                        Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left">
                                    其他：<asp:TextBox runat="server" ID="txt_HT_JSFS3" Width="96%" Rows="2" 
                                        TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" style="font-size: x-large;">
                                    <strong>合&nbsp;&nbsp;同&nbsp;&nbsp;签&nbsp;&nbsp;字</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    需方
                                </td>
                                <td align="center" colspan="2">
                                    供方 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    单位名称： 
                                </td>
                                <td>
                                    中材（天津）重型机械有限公司 
                                </td>
                                <td>
                                    单位名称 
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_GF1" runat="server" onfocus="this.blur()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    单位地址：
                                </td>
                                <td>
                                    天津市津围公路北辰科技园区华泰道2号 
                                </td>
                                <td>
                                    单位地址： 
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_DZ" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    法定代表人：
                                </td>
                                <td>
                                    彭明德
                                </td>
                                <td>
                                    法定代表人： 
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_FDDBR" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    委托代理人：
                                </td>
                                <td>
                                </td>
                                <td>
                                    委托代理人：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_WTDLR"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    电 话：
                                </td>
                                <td>
                                    022-86890109
                                </td>
                                <td>
                                    电 话：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_DH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    传 真：
                                </td>
                                <td>
                                    022-86890109
                                </td>
                                <td>
                                    传 真：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_CZ"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    开户银行：
                                </td>
                                <td>
                                    工商银行天津市财楼支行
                                </td>
                                <td>
                                    开户银行：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_KHYH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    帐 号：
                                </td>
                                <td>
                                    0302035309102231877
                                </td>
                                <td>
                                    帐 号：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_ZH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    税 号：
                                </td>
                                <td>
                                    120113718293930
                                </td>
                                <td>
                                    税 号：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_SH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    邮政编码：
                                </td>
                                <td>
                                    300409
                                </td>
                                <td>
                                    邮政编码：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_YB"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="采购订单">
            <ContentTemplate>
                <div>
                    <asp:TextBox runat="server" ID="txt_orderno" Width="200px"></asp:TextBox>
                    <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txt_orderno"
                        ServicePath="PC_ZDPP.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                        ServiceMethod="GetDDBH0" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </asp:AutoCompleteExtender>
                    <asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_OnClick" Text="新增订单" Height="30px"
                        BackColor="#97FFFF" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnZhuiJia" OnClick="btnZhuiJia_OnClick" Text="追加订单"
                        Height="30px" BackColor="#98FB98" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_OnClick" Text="删除订单"
                        Height="30px" BackColor="#CFCFCF" />
                </div>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Image runat="server" ID="Image1" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="font-size: xx-large;">
                            <strong>采&nbsp;&nbsp;购&nbsp;&nbsp;订&nbsp;&nbsp;单</strong>
                        </td>
                    </tr>
                </table>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="overflow: auto; width: 100%">
                            <%--height: 475px;--%>
                            <div class="cpbox xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater runat="server" ID="rptDD">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #48D1CC; height: 30px">
                                                <td>
                                                    <strong>序号</strong>
                                                </td>
                                                <td>
                                                    <strong>单据编号</strong>
                                                </td>
                                                <td>
                                                    <strong>制单人</strong>
                                                </td>
                                                <td>
                                                    <strong>制单日期</strong>
                                                </td>
                                                <td>
                                                    <strong>订单总金额</strong>
                                                </td>
                                                <td>
                                                    <strong>材料编码</strong>
                                                </td>
                                                <td>
                                                    <strong>物料名称</strong>
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
                                                <td>
                                                    <strong>类型</strong>
                                                </td>
                                                <td id="td11" runat="server">
                                                    <strong>数（重）量</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td id="td12" runat="server">
                                                    <strong>辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助单位</strong>
                                                </td>
                                                <td id="td13" runat="server">
                                                    <strong>长度</strong>
                                                </td>
                                                <td id="td14" runat="server">
                                                    <strong>宽度</strong>
                                                </td>
                                                <td id="td15" runat="server">
                                                    <strong>片/支</strong>
                                                </td>
                                                <td id="td16" runat="server">
                                                    <strong>含税单价</strong>
                                                </td>
                                                <td>
                                                    <strong>含税金额</strong>
                                                </td>
                                                <td>
                                                    <strong>交货日期</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <td>
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_XuHao" Text='<%#Container.ItemIndex + 1%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_orderno" Text='<%#Eval("orderno")%>'></asp:Label><%--订单编号--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_zdrnm" Text='<%#Eval("zdrnm")%>'></asp:Label><%--制单人--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_zdtime" Text='<%#Convert.ToDateTime(Eval("zdtime")).ToString("yyyy-MM-dd")%>'></asp:Label><%--制单时间--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_PO_ZJE" Text='<%#Eval("PO_ZJE")%>'></asp:Label><%--总金额--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_marid" Text='<%#Eval("marid")%>'></asp:Label><%--物料编码--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_marnm" Text='<%#Eval("marnm")%>'></asp:Label><%--物料名称--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_PO_TUHAO" Text='<%#Eval("PO_TUHAO")%>'></asp:Label><%--图号--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_margg" Text='<%#Eval("margg")%>'></asp:Label><%--规格--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_marcz" Text='<%#Eval("marcz")%>'></asp:Label><%--材质--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_margb" Text='<%#Eval("margb")%>'></asp:Label><%--国标--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_PO_MASHAPE" Text='<%#Eval("PO_MASHAPE")%>'></asp:Label><%--类型--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_zxnum" Text='<%#Eval("zxnum")%>'></asp:Label><%--数（重）量--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_marunit" Text='<%#Eval("marunit")%>'></asp:Label><%--单位--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_fznum" Text='<%#Eval("fznum")%>'></asp:Label><%--辅助数量--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_PO_TECUNIT" Text='<%#Eval("marfzunit")%>'></asp:Label><%--辅助单位--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_length" Text='<%#Eval("length")%>'></asp:Label><%--长度--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_width" Text='<%#Eval("width")%>'></asp:Label><%--宽度--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_PO_PZ" Text='<%#Eval("PO_PZ")%>'></asp:Label><%--片/支--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_ctprice" Text='<%#Eval("ctprice")%>'></asp:Label><%--含税单价--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_ctamount" Text='<%#Eval("ctamount")%>'></asp:Label><%--含税金额--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_cgtimerq" Text='<%#Eval("cgtimerq")%>'></asp:Label><%--交货日期--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="lb_totalnote" Width="200px" ToolTip='<%#Eval("detailnote")%>'
                                                        Text='<%#Eval("detailnote")%>'></asp:TextBox><%--备注--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="lb_ptcode" Width="300px" ToolTip='<%#Eval("ptcode")%>'
                                                        Text='<%#Eval("ptcode")%>'></asp:TextBox><%--计划跟踪号--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr style="height: 30px;">
                                                <td align="center" colspan="12">
                                                    汇&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总：
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_HZSL"></asp:Label>
                                                </td>
                                                <td colspan="7">
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lb_HZJE"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="30" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="true" ForeColor="Red">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="30" align="left">
                                            备注：<asp:TextBox runat="server" ID="txt_HT_DDBZ" TextMode="MultiLine" Rows="6" Width="90%"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审批">
            <ContentTemplate>
                <div style="width: 100%" align="center">
                    <asp:Panel runat="server" ID="panSP" Width="80%">
                        <asp:Panel runat="server" ID="tb">
                            <table width="80%">
                                <tr>
                                    <td align="right" id="tdSPLX">
                                        <asp:RadioButtonList ID="rbl_HT_SPLX" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                            onchange="rbl_HT_SPLX_rbl_HT_SPLX()">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="会审" Value="4" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Image runat="server" ID="Image2" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        采购合同审批
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb0">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr style="height: 25px">
                                    <td align="center" style="width: 20%">
                                        制单人：&nbsp;&nbsp;
                                        <asp:Label runat="server" ID="lb_HT_ZDR"></asp:Label>
                                    </td>
                                    <td>
                                        制单时间：&nbsp;&nbsp;
                                        <asp:Label runat="server" ID="lb_HT_ZDSJ"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        制单人建议:<br />
                                        <asp:TextBox ID="txt_HT_ZDR_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb1">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr style="height: 25px">
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR1" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR1_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR1_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR1_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb2">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR2" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR2" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR2_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR2_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR2_JY" runat="server" Height="42px" TextMode="MultiLine"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb3">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR3" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR3" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR3_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR3_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR3_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb4">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRCG" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image4" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr4()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRCG_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRCG_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRCG_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb5">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRShenC" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image8" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr5()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRShenC_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRShenC_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRShenC_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb6">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRJS" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image10" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr6()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRJS_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRJS_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRJS_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%--质量部新增--%>
                        <%--<asp:Panel runat="server" ID="tbnew">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRZL" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image15" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xrnew()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRZL_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRZL_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRZL_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                        <%--质量部新增--%>
                        <%--替换质量部新增--%>
                        <asp:Panel runat="server" ID="tb10">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRZ" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image14" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr10()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRZ_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRZ_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRZ_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <%--替换质量部新增--%>
                        <asp:Panel runat="server" ID="tb7">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRShiC" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image11" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr7()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRShiC_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRShiC_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRShiC_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb8">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRCW" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image12" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr8()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRCW_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRCW_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRCW_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb9">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHRFZ" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image13" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr9()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHRFZ_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHRFZ_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHRFZ_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        
                    </asp:Panel>
                </div>
                <div id="win" visible="false">
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    按部门查询：
                                </td>
                                <td>
                                    <input id="dep" name="dept" value="05">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 430px; height: 230px">
                        <table id="dg">
                        </table>
                    </div>
                </div>
                <div id="buttons" style="text-align: right" visible="false">
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                        保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                            onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                                data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
                    <input id="hidPerson" type="hidden" value="" />
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="Tab1" runat="server" HeaderText="采购部付款" Height="90%" Width="100%">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 10px">
                            <asp:Image ID="Image3" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                onClick="switchGridVidew(this,'yk')" Height="15px" Width="15px" runat="server" />
                        </td>
                        <td align="left">
                            付款记录
                        </td>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Text="已付金额（万元）:"></asp:Label>
                            <asp:Label ID="lb_lYFJE" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <input id="btnADDCR" type="button" class="button-outer" value="添加付款记录" onclick="return btnADDCR_onclick()"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <div id="yk" style="display: block;">
                    <asp:Panel ID="palYK" runat="server">
                        <asp:GridView ID="grvYKJL" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" ShowFooter="True" Style="white-space: normal">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_lIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BP_ID" HeaderText="付款单号">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BP_SCBHTH" HeaderText="市场部合同号">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BP_SHEBEI" HeaderText="设备名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BP_JE" HeaderText="付款金额（万元）">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BP_ZFBL" HeaderText="付款比例">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BP_YKRQ" HeaderText="付款日期" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <div style="width: 200px">
                                            <label>
                                                <%# Eval("BP_NOTE")%></label></div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编辑">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylfp" runat="server" CssClass="hand" onClick="BPEditDetail(this);"
                                            ToolTip='<%# Eval("BP_ID")%>'>
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BP_ID")%>' />
                                            编辑</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="删除">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("BP_ID") %>'
                                            OnClick="Lbtn_Del_OnClick" OnClientClick="javascript:return confirm('确定要删除吗？');">
                                            <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" runat="server" />删除
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" HorizontalAlign="Center" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Panel ID="palSWYK" runat="server" HorizontalAlign="Center">
                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                            没有记录!</asp:Panel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="Tab3" runat="server" HeaderText="发票单据" Height="90%" Width="100%">
            <ContentTemplate>
                <asp:Panel ID="palFP" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width: 10px">
                                <asp:Image ID="Image9" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                    onClick="switchGridVidew(this,'fp')" Height="15" Width="15" runat="server" />
                            </td>
                            <td>
                                发票记录
                            </td>
                            <td align="right">
                                <input id="btnAddFP" type="button" class="button-outer" value="添加发票记录" runat="server"
                                    onclick="return btnAddFP_onclick()" />
                            </td>
                        </tr>
                    </table>
                    <div id="fp" style="display: block;">
                        <asp:GridView ID="grvFP" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" ShowFooter="true">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_lIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BR_HTBH" HeaderText="合同编号" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BR_KPRQ" HeaderText="开票日期" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BR_KPJE" HeaderText="开票金额" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BR_ENGNAME" HeaderText="产品名称" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BR_FPDH" HeaderText="发票单号" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BR_JBR" HeaderText="经办人" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BR_SL" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="编辑">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylfp" runat="server" CssClass="hand" onClick="BREditDetail(this);"
                                            ToolTip='<%# Eval("BR_ID")%>'>
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BR_ID")%>' />
                                            编辑</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="查看">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylpz" runat="server" CssClass="hand" onClick="BRViewDetail(this);"
                                            ToolTip='<%# Eval("BR_ID")%>'>
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Assets/images/res.gif" ToolTip='<%# Eval("BR_ID")%>' />
                                            查看</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="删除">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkdel_FP" runat="server" ForeColor="Red" CommandArgument='<%# Eval("BR_ID")%>'
                                            OnClick="linkdel_FP_Click" OnClientClick="return confirm('确定要删除此记录吗？？？');">
                    删除</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="palFPJL" runat="server" HorizontalAlign="Center">
                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                            没有记录!</asp:Panel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="Tab5" runat="server" HeaderText="补充协议" Height="90%" Width="100%">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 10px">
                            <asp:Image ID="Image5" Style="cursor: hand" ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif"
                                onClick="switchGridVidew(this,'bcxy')" Height="15" Width="15" runat="server" />
                        </td>
                        <td align="left">
                            补充协议记录
                        </td>
                        <td align="right">
                            <input id="add_bcxy" type="button" runat="server" class="button-outer" value="添加补充协议"
                                onclick="add_bcxy_onclick()" />
                        </td>
                    </tr>
                </table>
                <div id="bcxy" style="display: block">
                    <asp:GridView ID="gvBCXY" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        CssClass="toptable grid" ForeColor="#333333" Width="100%" EmptyDataText="没有记录">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="lb_lIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="HT_XFHTBH" HeaderText="补充协议编号">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HT_ZDR" HeaderText="制单人">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HT_ZDSJ" HeaderText="制单时间">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HT_HTZJ" HeaderText="协议金额">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HT_GF" HeaderText="合同供方">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
