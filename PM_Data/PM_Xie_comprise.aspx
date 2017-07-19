<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Xie_comprise.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_comprise" %>

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
            window.showModalDialog('TBMP_Supply.aspx?sheetno=' + name, '', "dialogHeight:400px;dialogWidth:700px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PM_Xie_comprise.aspx?sheetno=" + name;
        }
    </script>
 <script language="javascript" type="text/javascript">
     function viewCondition() {
         document.getElementById("<%=PanelCondition1.ClientID%>").style.display = 'block';
     }
     function calcu(obj) {
         var a = obj.parentNode.parentNode;
         var con = a.getElementsByTagName('span');
         var b = con[8].innerHTML;
         var con1 = a.getElementsByTagName('input');
         var n = 0;
         for (var i = 0; i < con1.length; i++) {
             if (con1[i].id == obj.id) {
                 n = i;
             }
         }
         con1[n + 1].value = obj.value * b;
     }
     function drapDown(obj, col) {
         var a = $(obj).val();
         $(col).val(a)
     }
    </script>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {

            chk = document.getElementById("ctl00_PrimaryContent_CheckBox8");
            if (chk.checked == true) {
                var num = "<%=globnum1%>";
            }
            else {
                var num = "<%=globnum1%>";
                num = num - 1;
            }

            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 2,
                fixedCols: num,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
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
                                    fixedRow.style.backgroundColor = "#ffffff";
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
            $("div.sFData>div.sFDataInner>table input:text").remove();
            $("div.sFData>div.sFDataInner>table select").remove();
        }
       
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
                            
                                <input id="Button1" type="button" value="填写供应商" onclick="openLink(this)" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_autobj" runat="server" Text="比价保存" OnClick="btn_autobj_Click" />
                                &nbsp;&nbsp;
                                <%--<asp:Button ID="btn_BZJ" runat="server" Text="比总价保存" OnClick="btn_BZJ_Click" Visible="false" />--%>
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="保存" OnClick="btn_confirm_Click" />
                                &nbsp;&nbsp;
                                <%--<asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_Click" Visible="false" />--%>
                                &nbsp;&nbsp;
                                <%--<asp:Button ID="btn_change" runat="server" Text="变更" OnClick="btn_change_Click" Visible="false" />--%>
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;
                                <asp:Label ID="Labelerror" runat="server" Text="" Visible="False" ForeColor="Red"></asp:Label>
                                <%--<asp:HyperLink ID="Hyp_print" runat="server" Target="_blank">
                                    <asp:Image ID="Img_print" runat="server" Visible="false" ImageUrl="~/Assets/icon-fuction/89.gif"
                                        title="打印" /></asp:HyperLink>--%>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition1" runat="server" Width="60%" Style="display: none">
                       <%-- <asp:UpdatePanel ID="UpdatePanelCondition1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
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
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
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
                                            <table ID="tab" class="nowrap cptable fullwidth" width="100%">
                                                <asp:Repeater ID="comparepriceRepeater" runat="server" 
                                                    OnItemDataBound="comparepriceRepeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td ID="wlxx" runat="server" colspan="17" rowspan="1">
                                                                外协物料 
                                                            </td>
                                                            <td colspan="1" rowspan="1">
                                                                比价结果 
                                                            </td>
                                                            <td ID="gys1" runat="server" colspan="3" rowspan="1">
                                                                供应商1
                                                                <asp:Label ID="GYShang1" runat="server"></asp:Label>
                                                            </td>
                                                            <td ID="gys2" runat="server" colspan="3" rowspan="1">
                                                                供应商2
                                                                <asp:Label ID="GYShang2" runat="server"></asp:Label>
                                                            </td>
                                                            <td ID="gys3" runat="server" colspan="3" rowspan="1">
                                                                供应商3
                                                                <asp:Label ID="GYShang3" runat="server"></asp:Label>
                                                            </td>
                                                            <td ID="gys4" runat="server" colspan="3" rowspan="1">
                                                                供应商4
                                                                <asp:Label ID="GYShang4" runat="server"></asp:Label>
                                                            </td>
                                                            <td ID="gys5" runat="server" colspan="3" rowspan="1">
                                                                供应商5
                                                                <asp:Label ID="GYShang5" runat="server"></asp:Label>
                                                            </td>
                                                            <td ID="gys6" runat="server" colspan="3" rowspan="1">
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
                                                            <td ID="marmingcheng" runat="server">
                                                                <strong>物料名称</strong>
                                                            </td>
                                                            <td ID="martuhao" runat="server">
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td ID="marguige" runat="server">
                                                                <strong>规格</strong>
                                                            </td>
                                                            <td ID="marcaizhi" runat="server">
                                                                <strong>材质</strong>
                                                            </td>
                                                            <td ID="marguobiao" runat="server">
                                                                <strong>国标</strong>
                                                            </td>
                                                            <td ID="mashape" runat="server">
                                                                <strong>类型</strong>
                                                            </td>
                                                            <td ID="Td1" runat="server" visible="false">
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td ID="cgshuliang" runat="server">
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td ID="danwei" runat="server">
                                                                <strong>单位</strong>
                                                            </td>
                                                            <td ID="jine" runat="server">
                                                                <strong>金额</strong>
                                                            </td>
                                                            <td ID="fznum1" runat="server" visible="false">
                                                                <strong>辅助数量</strong>
                                                            </td>
                                                            <td ID="zxfznum1" runat="server">
                                                                <strong>执行辅助数量</strong>
                                                            </td>
                                                            <td ID="fzunit1" runat="server">
                                                                <strong>辅助单位</strong>
                                                            </td>
                                                            <td ID="length" runat="server">
                                                                <strong>长度</strong>
                                                            </td>
                                                            <td ID="width" runat="server">
                                                                <strong>宽度</strong>
                                                            </td>
                                                            <td ID="beizhu" runat="server">
                                                                <strong>备注</strong>
                                                            </td>
                                                            <td ID="shuilv" runat="server">
                                                                <strong>税率(%)</strong>
                                                            </td>
                                                            <td>
                                                                <strong>比价结果</strong>
                                                            </td>
                                                            <td ID="dyc1" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec1" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="dsc1" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc2" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec2" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="dsc2" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc3" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec3" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="dsc3" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc4" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec4" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="dsc4" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc5" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec5" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="dsc5" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                            <td ID="dyc6" runat="server">
                                                                <strong>交货期</strong>
                                                            </td>
                                                            <td ID="dec6" runat="server">
                                                                <strong>单价(含税)</strong>
                                                            </td>
                                                            <td ID="dsc6" runat="server">
                                                                <strong>金额(含税)</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <FooterTemplate>
                                                        <tr align="center">
                                                            <td ID="foot1" runat="server" colspan="10" rowspan="1">
                                                                汇总(RMB/元)
                                                            </td>
                                                             <td id="tk2" runat="server">
                                                                <asp:Label  ID="Lb_amount" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td colspan="4">
                                                                 <asp:Label visible="false" ID="Label1" runat="server" ></asp:Label>
                                                                 <asp:Label visible="false" ID="Label2" runat="server"></asp:Label>
                                                            </td>
                                                           <%-- <td ID="zjg12" runat="server" align="right" colspan="2" width="90px">
                                                            </td>--%>
                                                            <td ID="zjg13" runat="server" width="90px" colspan="3">
                                                                <asp:TextBox ID="totalTextBox13" runat="server" BorderStyle="None" 
                                                                    Enabled="False" Visible="false" Width="90px"></asp:TextBox>
                                                            </td>
                                                          <%--  <td ID="zjg22" runat="server" align="right" colspan="2" width="90px">
                                                            </td>--%>
                                                            <td ID="zjg23" runat="server" width="90px" colspan="3">
                                                                <asp:TextBox ID="totalTextBox23" runat="server" BorderStyle="None" 
                                                                    Enabled="False" Visible="false" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <%--<td ID="zjg32" runat="server" align="right" colspan="2" width="90px">
                                                            </td>--%>
                                                            <td ID="zjg33" runat="server" width="90px" colspan="3">
                                                                <asp:TextBox ID="totalTextBox33" runat="server" BorderStyle="None" 
                                                                    Enabled="False" Visible="false" Width="90px"></asp:TextBox>
                                                            </td>
                                                           <%-- <td ID="zjg42" runat="server" align="right" colspan="2" width="90px">
                                                            </td>--%>
                                                            <td ID="zjg43" runat="server" width="90px" colspan="3">
                                                                <asp:TextBox ID="totalTextBox43" runat="server" BorderStyle="None" 
                                                                    Enabled="False" Visible="false" Width="90px"></asp:TextBox>
                                                            </td>
                                                           <%-- <td ID="zjg52" runat="server" align="right" colspan="2" width="90px">
                                                            </td>--%>
                                                            <td ID="zjg53" runat="server" width="90px" colspan="3">
                                                                <asp:TextBox ID="totalTextBox53" runat="server" BorderStyle="None" 
                                                                    Enabled="False" Visible="false" Width="90px"></asp:TextBox>
                                                            </td>
                                                           <%-- <td ID="zjg62" runat="server" align="right" colspan="2" width="90px">
                                                            </td>--%>
                                                            <td ID="zjg63" runat="server" width="90px" colspan="3">
                                                                <asp:TextBox ID="totalTextBox63" runat="server" BorderStyle="None" 
                                                                    Enabled="False" Visible="false" Width="90px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <tr ID="row" runat="server" class="baseGadget">
                                                            <td>
                                                                <asp:CheckBox ID="CKBOX_SELECT" runat="server" BorderStyle="None" 
                                                                    Checked="false" CssClass="checkBoxCss" />
                                                                &nbsp;
                                                                <asp:Label ID="PIC_PCODE" runat="server" Text='<%#Eval("PIC_ID")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" 
                                                                    Text="<%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*50%>"></asp:Label>
                                                            </td>
                                                            <td ID="marmingchen1" runat="server">
                                                                <asp:Label ID="PIC_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                            </td>
                                                            <td ID="martuhao1" runat="server">
                                                                <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="PIC_MARGUIGE" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                                            </td>
                                                            <td ID="marcaizhi1" runat="server">
                                                                <asp:Label ID="PIC_MARCAIZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                            </td>
                                                            <td ID="marguobiao1" runat="server">
                                                                <asp:Label ID="PIC_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="mashape1" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                            </td>
                                                            <td ID="Td4" runat="server" visible="false">
                                                                <asp:Label ID="PIC_QUANTITY" runat="server" Text='<%#Eval("marnum")%>'></asp:Label>
                                                            </td>
                                                            <td ID="cgshuliang1" runat="server">
                                                                <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                            </td>
                                                            <td ID="danwwei1" runat="server">
                                                                <asp:Label ID="PIC_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                            </td>
                                                            <td ID="jine1" runat="server">
                                                                <asp:Label ID="Amount" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                            </td>
                                                            <td ID="fznum2" runat="server" visible="false">
                                                                <asp:Label ID="PIC_FZNUM" runat="server" Text='<%#Eval("marfznum")%>'></asp:Label>
                                                            </td>
                                                            <td ID="zxfznum2" runat="server">
                                                                <asp:Label ID="PIC_ZXFUNUM" runat="server" Text='<%#Eval("marzxfznum")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="PIC_ZXFUNUM" runat="server" Text='<%#Eval("marzxfznum")%>' Width="60px" Enabled="false"></asp:TextBox>--%>
                                                            </td>
                                                            <td ID="fzunit2" runat="server">
                                                                <asp:Label ID="PIC_FZUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                            </td>
                                                            <td ID="length1" runat="server">
                                                                <asp:Label ID="PIC_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                            </td>
                                                            <td ID="width1" runat="server">
                                                                <asp:Label ID="PIC_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                            </td>
                                                            <td ID="beizhu1" runat="server" style="width: 200px">
                                                                <%--<asp:TextBox ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>' BorderStyle="None"></asp:TextBox>--%>
                                                                <asp:Label ID="PIC_NOTE" runat="server" Text='<%#Eval("detailnote")%>' 
                                                                    Width="200px"></asp:Label>
                                                                <%--<input id="PIC_NOTE" type="text"  runat="server" value='<%#Eval("detailnote")%>' />--%>
                                                            </td>
                                                            <td ID="shuilv1" runat="server">
                                                                <asp:TextBox ID="PIC_SHUILV" runat="server" Text='<%#Eval("shuilv")%>' 
                                                                    Width="50px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="Drp_supplier" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="PIC_SUPPLIERRESID" runat="server" BorderStyle="None" 
                                                                    Text='<%#Eval("supplierresid")%>' Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td ID="jg11" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox11" runat="server" BorderStyle="None" AutoPostBack="true" class="rq1"
                                                                onkeydown="grControlFocus(this)"    Text='<%#Eval("qoutefstsa")%>' Width="80px"></asp:TextBox>
                                                                   
                                                            </td>
                                                            <td ID="jg12" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox12" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" 
                                                                    Text='<%#Eval("qoutescdsa")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                                    ControlToValidate="TextBox12" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg13" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox13" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutelstsa")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                    ControlToValidate="TextBox13" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg21" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox21" runat="server" BorderStyle="None" class="rq2"
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutefstsb")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td ID="jg22" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox22" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" 
                                                                    Text='<%#Eval("qoutescdsb")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                                                    ControlToValidate="TextBox22" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg23" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox23" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutelstsb")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                                                                    ControlToValidate="TextBox23" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg31" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox31" runat="server" BorderStyle="None" class="rq3"
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutefstsc")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td ID="jg32" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox32" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)"  Text='<%#Eval("qoutescdsc")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                                                    ControlToValidate="TextBox32" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg33" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox33" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutelstsc")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                                                    ControlToValidate="TextBox33" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg41" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox41" runat="server" BorderStyle="None" class="rq4"
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutefstsd")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td ID="jg42" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox42" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" 
                                                                    Text='<%#Eval("qoutescdsd")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                                                                    runat="server" ControlToValidate="TextBox42" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg43" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox43" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutelstsd")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" 
                                                                    runat="server" ControlToValidate="TextBox43" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg51" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox51" runat="server" BorderStyle="None" class="rq5"
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutefstse")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td ID="jg52" runat="server" width="110px">
                                                                <asp:TextBox ID="TextBox52" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" 
                                                                    Text='<%#Eval("qoutescdse")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                                                                    runat="server" ControlToValidate="TextBox52" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg53" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox53" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutelstse")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" 
                                                                    runat="server" ControlToValidate="TextBox53" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg61" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox61" runat="server" BorderStyle="None"  class="rq6"
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutefstsf")%>' Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td ID="jg62" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox62" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" onkeyup="calcu(this)" 
                                                                    Text='<%#Eval("qoutescdsf")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" 
                                                                    runat="server" ControlToValidate="TextBox62" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td ID="jg63" runat="server" width="100px">
                                                                <asp:TextBox ID="TextBox63" runat="server" BorderStyle="None" 
                                                                    onkeydown="grControlFocus(this)" Text='<%#Eval("qoutelstsf")%>' Width="80px"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" 
                                                                    runat="server" ControlToValidate="TextBox63" ErrorMessage="*" 
                                                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="37">
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="False">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="height:70px">
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
                                            </div>
                                            <div style="height:30px">
                                            <table align="center"  width="100%" >
                                                <tr>
                                                    <td>
                                                        制&nbsp;&nbsp;单&nbsp;&nbsp;人:<asp:TextBox ID="tb_zd" runat="server" Enabled="False"></asp:TextBox>
                                                     </td>
                                                     <td>
                                                        部&nbsp;门&nbsp;负&nbsp;责&nbsp;人:<asp:DropDownList ID="ddl_fuziren" runat="server" ></asp:DropDownList>
                                                        <%--<asp:ComboBox ID="cob_fuziren" runat="server"  AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Height="15px"  Width="120px">
                                                        </asp:ComboBox>--%>
                                                        </td>
                                                        <td>
                                                   主&nbsp;管&nbsp;经&nbsp;理:<asp:DropDownList ID="ddl_zgjl" runat="server"></asp:DropDownList>
                                                 <%--  <asp:ComboBox ID="cob_zgjl" runat="server"  AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Height="15px"  MaxLength="0" Width="120px">
                                                        </asp:ComboBox>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                         </div>
                                        </div>

                                        <script language="javascript" type="text/javascript">

                                            chk = document.getElementById("ctl00_PrimaryContent_CheckBox8");
                                            if (chk.checked == true) {
                                                var num = "<%=globnum1%>";
                                            }
                                            else {
                                                var num = "<%=globnum1%>";
                                                num = num - 1;
                                            }
                                            //        var num="<%=globnum1%>"; 
                                            var myST = new superTable("tab", {
                                                cssSkin: "tDefault",
                                                headerRows: 2,
                                                fixedCols: num,
                                                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                                onStart: function() {
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
                                                                    fixedRow.style.backgroundColor = "#ffffff";
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
                                            $("div.sFData>div.sFDataInner>table input:text").remove();
                                            $("div.sFData>div.sFDataInner>table select").remove();
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
