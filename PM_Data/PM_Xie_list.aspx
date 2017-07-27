<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="PM_Xie_list.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ѯ�ȼ۹���
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/pricesearch.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function redirectw(obj) {
            var sheetno;
            var num1;
            var num2;
            radio11 = document.getElementById("ctl00_PrimaryContent_rad_all");
            radio12 = document.getElementById("ctl00_PrimaryContent_rad_mypart");
            radio21 = document.getElementById("ctl00_PrimaryContent_rad_quanbu");
            radio22 = document.getElementById("ctl00_PrimaryContent_rad_weitijiao");
            radio23 = document.getElementById("ctl00_PrimaryContent_rad_shenhezhong");
            radio24 = document.getElementById("ctl00_PrimaryContent_rad_bohui");
            radio25 = document.getElementById("ctl00_PrimaryContent_rad_tongguo");
            radio26 = document.getElementById("ctl00_PrimaryContent_rad_wbj");
            radio27 = document.getElementById("ctl00_PrimaryContent_rad_ybj");
            radio28 = document.getElementById("ctl00_PrimaryContent_rad_yhg");
            if (radio11.checked == true) {
                num1 = 1;
            }
            else {
                num1 = 2;
            }

            if (radio21.checked == true) {
                num2 = 1;
            }
            else if (radio22.checked == true) {
                num2 = 2;
            }
            else if (radio23.checked == true) {
                num2 = 3;
            }
            else if (radio24.checked == true) {
                num2 = 4;
            }
            else if (radio25.checked == true) {
                num2 = 5;
            }
            else if (radio26.checked == true) {
                num2 = 6;
            }
            else if (radio27.checked == true) {
                num2 = 7;
            }
            else if (radio28.checked == true) {
                num2 = 8;
            }
            else if (radio29.checked == true) {
                num2 = 9;
            }
            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            window.open("PM_Xie_check_detail.aspx?num1=" + num1 + "&num2=" + num2 + "&sheetno=" + escape(sheetno) + "");
        }

        function mowinopen(ptcode_rcode) {
            var autonum = Math.round(10000 * Math.random());
            window.open('PM_Data_addto_list.aspx?autonum=' + autonum + '&ptcode_rcode=' + escape(ptcode_rcode) + '', '', "dialogHeight:460px;dialogWidth:780px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");

        }
    </script>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 0,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 2; i < j; i++) {
                        var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        if (coltext == coltextbef) {
                            for (var k = 2; k <= 9; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                            //                            for (var k = 6; k <= 10; k++) {
                            //                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            //                            }
                            //                            for (var k = 12; k <= 12; k++) {
                            //                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            //                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
    </script>

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
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                ѯ�ȼ��嵥�б�
                                <asp:RadioButton ID="rad_all" runat="server" Text="ȫ������" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_mypart" runat="server" Text="�ҵ�����" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                                <asp:TextBox ID="Tb_marrepcode" runat="server" Text="" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                ������Э���
                                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                    <asp:ListItem Text="ȫ��" Value="%" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="������Э" Value="������Э"></asp:ListItem>
                                    <asp:ListItem Text="��Ʒ��Э" Value="��Ʒ��Э"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RadioButton ID="rad_quanbu" runat="server" Text="ȫ��" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_quanbu_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_weitijiao" runat="server" Text="δ���" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_weitijiao_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_shenhezhong" runat="server" Text="�����" TextAlign="Right"
                                    GroupName="select" OnCheckedChanged="rad_shenhezhong_CheckedChanged" Checked="true"
                                    AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_bohui" runat="server" Text="�Ѳ���" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_bohui_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lb_bjdbh" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="rad_tongguo" runat="server" Text="��ͨ��" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_tongguo_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_wbj" runat="server" Text="δ����" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_ybj_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_ybj" runat="server" Text="�ѱ���" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_ybj_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_yhg" runat="server" Text="�Ѻϸ�" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_ybj_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_yjs" runat="server" Text="�ѽ���" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_ybj_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_add" runat="server" Height="25px" Width="70px" BorderColor="Pink"
                                    BorderStyle="Double" Text="׷�Ӷ���" OnClick="btn_add_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_xiatui" runat="server" Height="25px" Width="70px" BorderColor="Pink"
                                    BorderStyle="Double" Text="���ɶ���" OnClick="btn_xiatui_Click" />&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="cb_sp" runat="server" Visible="false" Checked="false" AutoPostBack="true"
                                    Text="ֻ��ʾ�ҵ�δ��" ForeColor="Red" OnCheckedChanged="CBSP_CheckedChanged" />
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
                            <td align="left">
                                ѯ�ȼ۵��ţ�
                                <asp:TextBox ID="Tb_pcode" runat="server" Text="" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                ��Ӧ�̣�
                                <asp:TextBox ID="tb_Gongyingshang" runat="server" Width="200px" OnTextChanged="tb_Gongyingshang_Textchanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_Gongyingshang"
                                    ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="2"
                                    CompletionInterval="10" ServiceMethod="GetWaixie" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td>
                                ����<asp:TextBox runat="server" ID="PIC_ZONGXU" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                ���ƣ�<asp:TextBox runat="server" ID="marnm"></asp:TextBox>
                            </td>
                            <td>
                                ÿҳ��ʾ��
                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="50����¼" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100����¼" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="150����¼" Value="150"></asp:ListItem>
                                    <asp:ListItem Text="200����¼" Value="200"></asp:ListItem>
                                    <asp:ListItem Text="250����¼" Value="200"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btn_input" runat="server" Text="�����ϸ��Ϣ" />
                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_input"
                                    PopupControlID="palscwx" Y="102" X="600" CancelControlID="close">
                                </asp:ModalPopupExtender>
                                <%--<asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="btn_input"
                                           PopupControlID="palscwx" Position="Bottom" OffsetX="-65" OffsetY="4" CacheDynamicResults="false">
                                        </asp:PopupControlExtender>--%>
                                <asp:Button ID="btn_export" runat="server" Text="ɸѡ" />
                                <%--<asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="btn_export"
                                            PopupControlID="select" Position="Bottom" OffsetX="-65" OffsetY="4" CacheDynamicResults="false">
                                        </asp:PopupControlExtender>--%>
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_export"
                                    PopupControlID="select" Y="102" X="600" CancelControlID="guan">
                                </asp:ModalPopupExtender>
                            </td>
                            <td>
                                <asp:Button ID="btn_baojian" runat="server" Text="����" OnClick="btn_baojian_click" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_search" runat="server" Text="��ѯ" OnClick="btn_search_click" />&nbsp;
                                <asp:Button ID="btn_clear" runat="server" Text="���" OnClick="btn_clear_click" />&nbsp;
                                <asp:Button ID="btn_daochu" runat="server" Text="����" OnClick="btn_daochu_Click" />&nbsp;
                                <asp:Button ID="btn_yuedaochu" runat="server" Text="���µ���" ForeColor="Red" />
                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btn_yuedaochu"
                                    PopupControlID="Panel1" Y="102" X="600" CancelControlID="guan1">
                                </asp:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="palscwx" Style="display: none; border-style: solid; border-width: 1px;
                    border-color: blue; background-color: White; position: absolute;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table width="250px">
                                <tr>
                                    <td>
                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                            font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                            <a id="close" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF;
                                                text-align: center; text-decoration: none; padding: 5px;" title="�ر�">X</a>
                                        </div>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        �ӹ����ţ�
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_jgnum" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <asp:Button ID="btnConfirm" runat="server" Text="ȷ ��" OnClick="btnConfirm_Click"
                                            UseSubmitBehavior="false" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnConfirm" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:Panel ID="select" Style="display: none; border-style: solid; border-width: 1px;
                    border-color: blue; background-color: Menu;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table width="400px">
                                <tr>
                                    <td>
                                        <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                            font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                            <a id="guan" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF; text-align: center;
                                                text-decoration: none; padding: 5px;" title="�ر�">X</a>
                                        </div>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60px">
                                        <asp:DropDownList ID="screen1" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:DropDownList ID="ddlRelation1" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                            <asp:ListItem Value="1">������</asp:ListItem>
                                            <asp:ListItem Value="2">����</asp:ListItem>
                                            <asp:ListItem Value="3">������</asp:ListItem>
                                            <asp:ListItem Value="4">����</asp:ListItem>
                                            <asp:ListItem Value="5">���ڻ����</asp:ListItem>
                                            <asp:ListItem Value="6">С��</asp:ListItem>
                                            <asp:ListItem Value="7">С�ڻ����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt1" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic1" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60px">
                                        <asp:DropDownList ID="screen2" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:DropDownList ID="ddlRelation2" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                            <asp:ListItem Value="1">������</asp:ListItem>
                                            <asp:ListItem Value="2">����</asp:ListItem>
                                            <asp:ListItem Value="3">������</asp:ListItem>
                                            <asp:ListItem Value="4">����</asp:ListItem>
                                            <asp:ListItem Value="5">���ڻ����</asp:ListItem>
                                            <asp:ListItem Value="6">С��</asp:ListItem>
                                            <asp:ListItem Value="7">С�ڻ����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt2" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic2" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60px">
                                        <asp:DropDownList ID="screen3" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:DropDownList ID="ddlRelation3" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                            <asp:ListItem Value="1">������</asp:ListItem>
                                            <asp:ListItem Value="2">����</asp:ListItem>
                                            <asp:ListItem Value="3">������</asp:ListItem>
                                            <asp:ListItem Value="4">����</asp:ListItem>
                                            <asp:ListItem Value="5">���ڻ����</asp:ListItem>
                                            <asp:ListItem Value="6">С��</asp:ListItem>
                                            <asp:ListItem Value="7">С�ڻ����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt3" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic3" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60px">
                                        <asp:DropDownList ID="screen4" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:DropDownList ID="ddlRelation4" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                            <asp:ListItem Value="1">������</asp:ListItem>
                                            <asp:ListItem Value="2">����</asp:ListItem>
                                            <asp:ListItem Value="3">������</asp:ListItem>
                                            <asp:ListItem Value="4">����</asp:ListItem>
                                            <asp:ListItem Value="5">���ڻ����</asp:ListItem>
                                            <asp:ListItem Value="6">С��</asp:ListItem>
                                            <asp:ListItem Value="7">С�ڻ����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt4" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic4" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60px">
                                        <asp:DropDownList ID="screen5" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:DropDownList ID="ddlRelation5" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                            <asp:ListItem Value="1">������</asp:ListItem>
                                            <asp:ListItem Value="2">����</asp:ListItem>
                                            <asp:ListItem Value="3">������</asp:ListItem>
                                            <asp:ListItem Value="4">����</asp:ListItem>
                                            <asp:ListItem Value="5">���ڻ����</asp:ListItem>
                                            <asp:ListItem Value="6">С��</asp:ListItem>
                                            <asp:ListItem Value="7">С�ڻ����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt5" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="60px">
                                        <asp:DropDownList ID="screen6" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" width="100px">
                                        <asp:DropDownList ID="ddlRelation6" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="0" Selected="True">����</asp:ListItem>
                                            <asp:ListItem Value="1">������</asp:ListItem>
                                            <asp:ListItem Value="2">����</asp:ListItem>
                                            <asp:ListItem Value="3">������</asp:ListItem>
                                            <asp:ListItem Value="4">����</asp:ListItem>
                                            <asp:ListItem Value="5">���ڻ����</asp:ListItem>
                                            <asp:ListItem Value="6">С��</asp:ListItem>
                                            <asp:ListItem Value="7">С�ڻ����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt6" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic6" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60px">
                                        ʱ�䣨��-�� XXXX-XX����
                                    </td>
                                    <td style="text-align: center; width: 100px">
                                        <asp:DropDownList ID="ddl_time" runat="server" BackColor="AliceBlue">
                                            <asp:ListItem Text="����" Value="=" Selected="True"></asp:ListItem>
                                            <%--<asp:ListItem Text="����" Value=">="></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_time" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <%--<td>
                                                  <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                                            <asp:ListItem Value="OR">����</asp:ListItem>
                                                            <asp:ListItem Value="AND" Selected="True">����</asp:ListItem>
                                                        </asp:DropDownList>
                                                  </td>--%>
                                </tr>
                                <%--<tr>
                                                  <td style="width:60px">
                                                   ʱ�䣨��-�£���
                                                  </td>
                                                  <td style="text-align:center;width:100px">
                                                  <asp:DropDownList ID="ddl_time2" runat="server" BackColor="AliceBlue" >
                                                  <asp:ListItem Text="����" Value="=" Selected="True"></asp:ListItem>
                                                  <asp:ListItem Text="����" Value=">="></asp:ListItem>
                                                  </asp:DropDownList>
                                                  </td>
                                                    <td>
                                                      <asp:TextBox ID="Txt6" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                  <td>
                                                  </td>
                                                  </tr>--%>
                                <tr>
                                    <td colspan="4" style="text-align: right">
                                        <asp:Button ID="reset" runat="server" Text="�� ��" OnClick="reset_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnfind" runat="server" Text="�� ��" OnClick="search_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnfind" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:Panel ID="Panel1" Style="display: none; border-style: solid; border-width: 1px;
                    border-color: blue; background-color: Menu;" runat="server">
                    <table width="270px">
                        <tr>
                            <td colspan="3">
                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                    <a id="guan1" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF;
                                        text-align: center; text-decoration: none; padding: 5px;" title="�ر�">X</a>
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                ����<br />
                                ���£�
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txt_year" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                (���磺2014-06)
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Button ID="Button1" runat="server" Text="ȷ ��" OnClick="btn_yuedaochuclick" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="checked_list_Repeater" runat="server" OnItemDataBound="checked_list_Repeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>��<br />
                                                        ��</strong>
                                                </td>
                                                <td>
                                                    <strong>�ȼ۵���</strong>
                                                </td>
                                                <td>
                                                    <strong>�ӹ�����</strong>
                                                </td>
                                                <%--<td>
                                                <strong>�Ƿ����</strong>
                                                </td>--%>
                                                <td>
                                                    <strong>����<br />
                                                        ������</strong>
                                                </td>
                                                <td>
                                                    <strong>�Ƶ���</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>����<br />
                                                        ����</strong>
                                                </td>
                                                <td>
                                                    <strong>�ܽ��</strong>
                                                </td>
                                                <td>
                                                    <strong>���<br />
                                                        ��־</strong>
                                                </td>
                                                <td>
                                                    <strong>����<br />
                                                        ��־</strong>
                                                </td>
                                                <td>
                                                    <strong>���㵥��</strong>
                                                </td>
                                                <td>
                                                    <strong>�Ƿ񱨼�</strong>
                                                </td>
                                                <td>
                                                    <strong>��Ӧ��</strong>
                                                </td>
                                                <td>
                                                    <strong>��Э����</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>�����</strong>
                                                </td>
                                                <td>
                                                    <strong>��ͬ��</strong>
                                                </td>
                                                <td>
                                                    <strong>��Ŀ����</strong>
                                                </td>
                                                <%--<td>
                                                    <strong>�豸����</strong>
                                                </td>--%>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>ͼ��/��ʶ��</strong>
                                                </td>
                                                <td>
                                                    <strong>ͼ�����</strong>
                                                </td>
                                                <%--<td>
                                                    <strong>���</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>--%>
                                                <%--<td>
                                                    <strong>����</strong>
                                                </td>--%>
                                                <td>
                                                    <strong>��Э����</strong>
                                                </td>
                                                <td>
                                                    <strong>��Э����</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>����</strong>
                                                </td>
                                                <td>
                                                    <strong>��λ</strong>
                                                </td>
                                                <td>
                                                    <strong>���ձ���<br />
                                                        (��˰)</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                                ondblclick="redirectw(this)" title="��˫���鿴">
                                                <%--onclick="rowclick(this)"--%>
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList5.SelectedValue))%>'></asp:Label>
                                                    <asp:Label ID="PIC_ID" runat="server" Text='<%#Eval("PIC_ID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PTC" runat="server" Text='<%#Eval("PTC")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbdh" runat="server" Text='<%#Eval("picno")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_jgnum" runat="server" Text='<%#Eval("PIC_JGNUM") %>'></asp:Label>
                                                </td>
                                                <%--  <td>
                                                <asp:Label ID="lb_rkstatus" runat="server" Text='<%#Eval("PIC_RKSTATUS") %>'></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="bmfzrid" runat="server" Text='<%#Eval("iclfzrid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="bmfzrnm" runat="server" Text='<%#Eval("iclfzrnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zdid" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zdnm" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zdtime" runat="server" Text='<%#Eval("zdtime")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zgid" runat="server" Text='<%#Eval("iclzgid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zgnm" runat="server" Text='<%#Eval("iclzgnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="amount" runat="server" Text='<%#Eval("iclamount")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="shstate" runat="server" Text='<%#get_pur_bjdsh(Eval("totalstate").ToString())%>'></asp:Label>
                                                    <asp:Label ID="totalstate" runat="server" Text='<%#Eval("totalstate")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lab_orderstate" runat="server" Text='<%#get_orderstate(Eval("PIC_ORDERSTATE").ToString()) %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TA_TOTALNOTE" runat="server" Text='<%#Eval("TA_TOTALNOTE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lb_bjstatus" runat="server" Text='<%#get_bjstatus(Eval("PIC_BJSTATUS").ToString()) %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="supplierid" runat="server" Text='<%#Eval("supplierresid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WSID" runat="server" Text='<%#Eval("MS_WSID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_ENGID" runat="server" Text='<%#Eval("PIC_ENGID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="CM_CONTR" runat="server" Text='<%#Eval("CM_CONTR")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="CM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="PIC_ENGNAME" runat="server" Text='<%#Eval("PIC_ENGNAME")%>'></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_ZONGXU" runat="server" Text='<%#Eval("PIC_ZONGXU")%>'></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="margg" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="marcz" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                </td>--%>
                                                <%--<td>
                                                    <asp:Label ID="margb" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="ICL_WXTYPE" runat="server" Text='<%#Eval("ICL_WXTYPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_MASHAPE" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_UWGHT" runat="server" Text='<%#Eval("MS_UWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="num" runat="server" Text='<%#Eval("marzxnum")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="unit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lasprice" runat="server" Text='<%#Eval("detamount")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="20">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                û�м�¼��</asp:Panel>
                                        </td>
                                    </tr>
                                </table>

                                <script language="javascript" type="text/javascript">
                                    var myST = new superTable("tab", {
                                        cssSkin: "tDefault",
                                        headerRows: 1,
                                        fixedCols: 0,
                                        //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                        onStart: function() {
                                            //		        this.start = new Date();
                                        },
                                        onFinish: function() {
                                            for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 2; i < j; i++) {
                                                var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                                                var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                                                var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                                                if (coltext == coltextbef) {
                                                    for (var k = 2; k <= 9; k++) {
                                                        dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                                                    }
                                                    //                                                    for (var k = 6; k <= 10; k++) {
                                                    //                                                        dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                                                    //                                                    }
                                                    //                                                    for (var k = 12; k <= 12; k++) {
                                                    //                                                        dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                                                    //                                                    }
                                                }
                                            }
                                            //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                                        }
                                    });
                                </script>

                            </div>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            �����<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>���ȼ۵�/
                                            <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red"></asp:Label>����¼
                                        </td>
                                        <td>
                                            ɸѡ�������ձ����ܼƣ�<asp:Label runat="server" ID="lbBJZJ" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td align="right" width="50%">
                                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:Panel ID="Pan_select" runat="server">
                            <asp:CheckBox ID="selectall" runat="server" Text="ȫѡ" AutoPostBack="true" TextAlign="left"
                                Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                            <asp:Button ID="btn_LX" runat="server" Text="��ѡ" OnClick="btn_LX_click" />
                            <asp:Button ID="btn_QX" runat="server" Text="ȡ��" OnClick="btn_QX_click" />
                        </asp:Panel>
                        </div> </div> </div>
                        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 50%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            ���ݴ����У����Ժ�...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
