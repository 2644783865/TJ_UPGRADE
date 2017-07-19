<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_fayun_comprise.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_fayun_comprise" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../JS/jquery/jquery-1.4.2-vsdoc.js" type="text/javascript"></script>

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
            window.showModalDialog('TBMP_Supply_fayun.aspx?sheetno=' + name, '', "dialogHeight:400px;dialogWidth:700px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PM_fayun_comprise.aspx?sheetno=" + name;

        }
    </script>

    <script language="javascript" type="text/javascript">
        function viewCondition() {
            document.getElementById("<%=PanelCondition1.ClientID%>").style.display = 'block';
        }
        function check_num(obj) {
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,6})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0.00";
                obj.focus();
            }
        }
    function CalWeightLength(){
//console.log( $("#tab input[name*=PM_WEIGHT]"));
 $("#tab input[name*=PM_WEIGHT]").each(function(){
 var $tr=$(this).parent().parent();
 var weight=$tr.find("input[name*=PM_WEIGHT]").val();
  var length=$tr.find("input[name*=PM_LENGTH]").val();
   var money1=$tr.find("input[name*=TextBox13]").val();
   var avg1=(parseFloat(money1)/parseFloat(weight)/parseFloat(length)).toFixed(2);
   $tr.find("input[name*=PM_AVGA]").val(avg1);
   
   var money2=$tr.find("input[name*=TextBox23]").val();
   var avg2=(parseFloat(money2)/parseFloat(weight)/parseFloat(length)).toFixed(2);
   $tr.find("input[name*=PM_AVGB]").val(avg2);

  var money3=$tr.find("input[name*=TextBox33]").val();
   var avg3=(parseFloat(money3)/parseFloat(weight)/parseFloat(length)).toFixed(2);
   $tr.find("input[name*=PM_AVGC]").val(avg3);
   
   var money4=$tr.find("input[name*=TextBox43]").val();
   var avg4=(parseFloat(money4)/parseFloat(weight)/parseFloat(length)).toFixed(2);
   $tr.find("input[name*=PM_AVGD]").val(avg4);
   
   var money5=$tr.find("input[name*=TextBox53]").val();
   var avg5=(parseFloat(money5)/parseFloat(weight)/parseFloat(length)).toFixed(2);
   $tr.find("input[name*=PM_AVGE]").val(avg5);
   
   var money6=$tr.find("input[name*=TextBox63]").val();
   var avg6=(parseFloat(money6)/parseFloat(weight)/parseFloat(length)).toFixed(2);
   $tr.find("input[name*=PM_AVGF]").val(avg6);
 
 });
    }
    </script>

    <%-- <script language="javascript" type="text/javascript">  
   Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)
        {
            
        }
        function EndRequestHandler(sender, args)
        {
            
           chk=document.getElementById("ctl00_PrimaryContent_CheckBox8");
           if(chk.checked==true)
           {
              var num="<%=globnum1%>";
           }
           else
           {
               var num="<%=globnum1%>";
               num=num-1;
           }
           
           var myST = new superTable("tab", {
	        cssSkin : "tDefault",
	        headerRows : 2,
	        fixedCols : num,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {
		        for (var i=0, j=this.sDataTable.tBodies[0].rows.length-1; i<j; i++) 
                {
                   
                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function (i) 
                    {
                        var clicked = false;

                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                        return function () 
                              {
                                    if (clicked) 
                                    {
                                        dataRow.style.backgroundColor = "#ffffff";
                                        fixedRow.style.backgroundColor = "#ffffff";
                                        clicked = false;
                                    }
                                    else 
                                    {
                                        dataRow.style.backgroundColor = "#FFC125";
                                        fixedRow.style.backgroundColor = "#FFC125";
                                        clicked = true;
                                    }
                                }
                                }.call(this, i);
                            }
                         return this;
	        }
        });
      $("div.sFData>div.sFDataInner>table input:text").remove();
      $("div.sFData>div.sFDataInner>table select").remove();
        }
       
    </script>--%>

    <script type="text/javascript">
        $(function() {
            var name = 'init';
            var i = 0;
            var n = 1;
            $.each($("#tab tr"), function(key) {

                if (!isNaN($(this).children().eq(1).html())) {

                    if ($(this).children().eq(1).html() != 1 && $(this).children().eq(1).html() != null) {
                        $(this).children().eq(11).attr("style", "display:none");
                        $(this).children().eq(12).attr("style", "display:none");
                        $(this).children().eq(13).attr("style", "display:none");
                        $(this).children().eq(14).attr("style", "display:none");
                        $(this).children().eq(15).attr("style", "display:none");
                        $(this).children().eq(16).attr("style", "display:none");
                        $(this).children().eq(17).attr("style", "display:none");
                        $(this).children().eq(18).attr("style", "display:none");
                        $(this).children().eq(19).attr("style", "display:none");
                        $(this).children().eq(20).attr("style", "display:none");
                        $(this).children().eq(21).attr("style", "display:none");
                        $(this).children().eq(22).attr("style", "display:none");
                        $(this).children().eq(23).attr("style", "display:none");
                        $(this).children().eq(24).attr("style", "display:none");
                        n++;
                        $($("#tab tr").eq(2)).children().eq(11).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(12).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(13).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(14).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(15).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(16).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(17).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(18).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(19).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(20).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(21).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(22).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(23).attr("rowspan", n);
                        $($("#tab tr").eq(2)).children().eq(24).attr("rowspan", n);
                    }
                }
            });
        });
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
                                <input id="Button1" type="button" value="填写发运商" onclick="openLink(this)" />
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
                                <%--<asp:Button ID="btn_change" runat="server" Text="变更" OnClick="btn_change_Click" Visible="false" />
                                &nbsp;&nbsp;--%>
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;
                                <asp:Label ID="Labelerror" runat="server" Text="" Visible="False" ForeColor="Red"></asp:Label>
                                <%-- <asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                    <asp:Image ID="Img_print" runat="server" Visible="false" ImageUrl="~/Assets/icon-fuction/89.gif"
                                        title="打印" /></asp:HyperLink>
                                &nbsp;&nbsp;&nbsp;--%>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition1" runat="server" Width="60%" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table id="tab1" class="nowrap cptable fullwidth" width="100%">
                                    <tr>
                                        <td>
                                            发运商1
                                        </td>
                                        <td>
                                            发运商2
                                        </td>
                                        <td>
                                            发运商3
                                        </td>
                                        <td>
                                            发运商4
                                        </td>
                                        <td>
                                            发运商5
                                        </td>
                                        <td>
                                            发运商6
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="PM_SUPPLIERANAME" runat="server" Text="" onblur="providerchanged(0)"></asp:TextBox>
                                            <asp:Label ID="LbA_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PM_SUPPLIERAID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PM_SUPPLIERA" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PM_SUPPLIERBNAME" runat="server" Text="" onblur="providerchanged(1)"></asp:TextBox>
                                            <asp:Label ID="LbB_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PM_SUPPLIERBID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PM_SUPPLIERB" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PM_SUPPLIERCNAME" runat="server" Text="" onblur="providerchanged(2)"></asp:TextBox>
                                            <asp:Label ID="LbC_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PM_SUPPLIERCID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PM_SUPPLIERC" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PM_SUPPLIERDNAME" runat="server" Text="" onblur="providerchanged(3)"></asp:TextBox>
                                            <asp:Label ID="LbD_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PM_SUPPLIERDID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PM_SUPPLIERD" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PM_SUPPLIERENAME" runat="server" Text="" onblur="providerchanged(4)"></asp:TextBox>
                                            <asp:Label ID="LbE_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PM_SUPPLIEREID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PM_SUPPLIERE" runat="server" Text="" Style="display: none"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PM_SUPPLIERFNAME" runat="server" Text="" onblur="providerchanged(5)"></asp:TextBox>
                                            <asp:Label ID="LbF_lei" runat="server" Text="" Style="display: none"></asp:Label>
                                            <asp:TextBox ID="PM_SUPPLIERFID" runat="server" Text="" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="PM_SUPPLIERF" runat="server" Text="" Style="display: none"></asp:TextBox>
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
                                <td id="Td1" visible="false" runat="server">
                                    日期:
                                    <asp:TextBox ID="tb_time" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="询比价单">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 600px">
                                        <div class="cpbox8 xscroll">
                                            <table id="tab" class="nowrap cptable fullwidth" width="100%">
                                                <asp:Repeater ID="comparepriceRepeater" runat="server" OnItemDataBound="comparepriceRepeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td id="wlxx" runat="server" colspan="15" rowspan="1">
                                                                发运信息
                                                            </td>
                                                            <td colspan="1" rowspan="1">
                                                                比价结果
                                                            </td>
                                                            <td id="gys1" runat="server" colspan="3" rowspan="1">
                                                                发运商1
                                                                <asp:Label ID="GYShang1" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys2" runat="server" colspan="3" rowspan="1">
                                                                发运商2
                                                                <asp:Label ID="GYShang2" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys3" runat="server" colspan="3" rowspan="1">
                                                                发运商3
                                                                <asp:Label ID="GYShang3" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys4" runat="server" colspan="3" rowspan="1">
                                                                发运商4
                                                                <asp:Label ID="GYShang4" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys5" runat="server" colspan="3" rowspan="1">
                                                                发运商5
                                                                <asp:Label ID="GYShang5" runat="server"></asp:Label>
                                                            </td>
                                                            <td id="gys6" runat="server" colspan="3" rowspan="1">
                                                                发运商6
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
                                                            <td>
                                                                <strong>编号</strong>
                                                            </td>
                                                            <td id="td1" runat="server">
                                                                <strong>合同号</strong>
                                                            </td>
                                                            <td id="td2" runat="server">
                                                                <strong>项目名称</strong>
                                                            </td>
                                                            <td id="td3" runat="server">
                                                                <strong>任务号</strong>
                                                            </td>
                                                            <td id="td4" runat="server">
                                                                <strong>设备名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>发货数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>吨</strong>
                                                            </td>
                                                            <td>
                                                                <strong>公里</strong>
                                                            </td>
                                                            <td id="td5" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="td6" runat="server">
                                                                <strong>收货单位</strong>
                                                            </td>
                                                            <td>
                                                                <strong>收获地址</strong>
                                                            </td>
                                                            <td id="td7" runat="server">
                                                                <strong>图号</strong>
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
                                                            <td id="dsc1" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td runat="server" id="tg1">
                                                                <strong>吨/公里</strong>
                                                            </td>
                                                            <td id="dyc2" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dsc2" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                             <td runat="server" id="tg2">
                                                                <strong>吨/公里</strong>
                                                            </td>
                                                            <td id="dyc3" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dsc3" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td runat="server" id="tg3">
                                                                <strong>吨/公里</strong>
                                                            </td>
                                                            <td id="dyc4" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dsc4" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td runat="server" id="tg4">
                                                                <strong>吨/公里</strong>
                                                            </td>
                                                            <td id="dyc5" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dsc5" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td runat="server" id="tg5">
                                                                <strong>吨/公里</strong>
                                                            </td>
                                                            <td id="dyc6" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td id="dsc6" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td runat="server" id="tg6">
                                                                <strong>吨/公里</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id="row" runat="server" class="baseGadget">
                                                            <td>
                                                                <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" Checked="false"
                                                                    CssClass="checkBoxCss" />
                                                                <asp:HiddenField runat="server" ID="PM_ID" Value='<%#Eval("PM_ID")%>' />
                                                                <asp:HiddenField runat="server" ID="PM_FATHERID" Value='<%#Eval("PM_FATHERID")%>' />
                                                                &nbsp;
                                                            </td>
                                                            <td id="index" runat="server" class="dep">
                                                                <%--<asp:Label ID="rownum" runat="server" Text="<%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*50%>"></asp:Label>--%>
                                                                <%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*50%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="CM_BIANHAO" runat="server" Text='<%#Eval("CM_BIANHAO")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt1" runat="server">
                                                                <asp:Label ID="CM_CONTR" runat="server" Text='<%#Eval("CM_CONTR")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt2" runat="server">
                                                                <asp:Label ID="CM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt3" runat="server">
                                                                <asp:Label ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt4" runat="server">
                                                                <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%#Eval("TSA_ENGNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PM_FYNUM" runat="server" Text='<%#Eval("PM_FYNUM")%>'></asp:Label>
                                                            </td>
                                                             <td>
                                                                <asp:TextBox ID="PM_WEIGHT" runat="server" Text='<%#Eval("PM_WEIGHT")%>' Width="80px"
                                                                    onblur="javascript:check_num(this)" onkeyup="CalWeightLength()" name="PM_WEIGHT"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_LENGTH" runat="server" Text='<%#Eval("PM_LENGTH")%>' Width="80px"
                                                                    onblur="javascript:check_num(this)" name="PM_LENGTH" onkeyup="CalWeightLength()"></asp:TextBox>
                                                            </td>
                                                            <td id="tt5" runat="server">
                                                                <asp:Label ID="CM_JHTIME" runat="server" Text='<%#Eval("CM_JHTIME")%>'></asp:Label>
                                                            </td>
                                                            <td id="tt6" runat="server">
                                                                <asp:Label ID="CM_CUSNAME" runat="server" Text='<%#Eval("CM_CUSNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_ADDRESS" runat="server" Text='<%#Eval("PM_ADDRESS")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td id="tt7" runat="server">
                                                                <%-- <asp:TextBox ID="txt_neirong" runat="server" Text='<%#Eval("PM_FHDETAIL")%>'></asp:TextBox>--%>
                                                                <asp:Label ID="TSA_MAP" runat="server" Text='<%#Eval("TSA_MAP")%>'></asp:Label>
                                                            </td>
                                                            <td id="shuilv1" runat="server">
                                                                <asp:TextBox ID="PM_SHUILV" runat="server" Text='<%#Eval("PM_SHUILV")%>' Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drp_supplier" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="PM_SUPPLIERRESID" runat="server" BorderStyle="None" Text='<%#Eval("PM_SUPPLIERRESID")%>'
                                                                    Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td id="jg11" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox11" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTEFSTSA")%>' Width="80px" ></asp:TextBox>
                                                            </td>
                                                            <td id="jg13" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox13" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTELSTSA")%>' Width="80px" onkeyup="CalWeightLength()"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox13"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_AVGA" runat="server" Text='<%#Eval("PM_AVGA")%>' Width="80px"
                                                                    name="PM_AVGA" onfocus="this.blur()"></asp:TextBox>
                                                            </td>
                                                            <td id="jg21" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox21" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTEFSTSB")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td id="jg23" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox23" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTELSTSB")%>' Width="80px" onkeyup="CalWeightLength()"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox23"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_AVGB" runat="server" Text='<%#Eval("PM_AVGB")%>' Width="80px" name="PM_AVGB" onfocus="this.blur()"></asp:TextBox>
                                                            </td>
                                                            <td id="jg31" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox31" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTEFSTSC")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td id="jg33" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox33" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTELSTSC")%>' Width="80px" onkeyup="CalWeightLength()"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TextBox33"
                                                                    ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_AVGC" runat="server" Text='<%#Eval("PM_AVGC")%>' Width="80px" name="PM_AVGC" onfocus="this.blur()"></asp:TextBox>
                                                            </td>
                                                            <td id="jg41" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox41" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTEFSTSD")%>' Width="80px" ></asp:TextBox>
                                                            </td>
                                                            <td id="jg43" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox43" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTELSTSD")%>' Width="80px" onkeyup="CalWeightLength()"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                                    ControlToValidate="TextBox43" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_AVGD" runat="server" Text='<%#Eval("PM_AVGD")%>' Width="80px" name="PM_AVGD" onfocus="this.blur()"></asp:TextBox>
                                                            </td>
                                                            <td id="jg51" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox51" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTEFSTSE")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td id="jg53" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox53" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTELSTSE")%>' Width="80px" onkeyup="CalWeightLength()"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                                    ControlToValidate="TextBox53" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PM_AVGE" runat="server" Text='<%#Eval("PM_AVGE")%>' Width="80px" name="PM_AVGE" onfocus="this.blur()"></asp:TextBox>
                                                            </td>
                                                            <td id="jg61" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox61" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTEFSTSF")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td id="jg63" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox63" runat="server" BorderStyle="None" onkeydown="grControlFocus(this)"
                                                                    Text='<%#Eval("PM_QOUTELSTSF")%>' Width="80px" onkeyup="CalWeightLength()"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                                                    ControlToValidate="TextBox63" ErrorMessage="*" ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                             <td>
                                                                <asp:TextBox ID="PM_AVGF" runat="server" Text='<%#Eval("PM_AVGF")%>' Width="80px" name="PM_AVGF" onfocus="this.blur()"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="26">
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="False">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="height: 40px">
                                            <table align="center" border="1" class="nowrap cptable fullwidth" width="100%">
                                                <tr>
                                                    <td>
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
                                                        页
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        制&nbsp;&nbsp;单&nbsp;&nbsp;人:<asp:TextBox ID="tb_zd" runat="server" Enabled="False"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        部&nbsp;门&nbsp;负&nbsp;责&nbsp;人:<asp:ComboBox ID="cob_fuziren" runat="server" AutoCompleteMode="SuggestAppend"
                                                            DropDownStyle="DropDownList" Height="15px" MaxLength="0" Width="120px">
                                                        </asp:ComboBox>
                                                    </td>
                                                    <td>
                                                        主&nbsp;管&nbsp;经&nbsp;理:<asp:ComboBox ID="cob_zgjl" runat="server" AutoCompleteMode="SuggestAppend"
                                                            DropDownStyle="DropDownList" Height="15px" MaxLength="0" Width="120px">
                                                        </asp:ComboBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </div>
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
