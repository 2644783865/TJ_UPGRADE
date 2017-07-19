<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_startcontent.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_startcontent"
    Title="物料需用计划" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <%-- <link type="text/css" href="../Assets/style.css" rel="stylesheet" />--%>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/pricesearch.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
 
    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">
        function xsuse(obj) {
            var marnm;
            var marcz;
            var ptcode;
            var marid;
            ptcode = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            marid = obj.getElementsByTagName("td")[4].getElementsByTagName("span")[0].innerHTML;
            marnm = obj.getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML;
            marcz = obj.getElementsByTagName("td")[7].getElementsByTagName("span")[0].innerHTML;
            windowopen("PC_Date_silimarmarshow.aspx?ptcode=" + ptcode + "&marid=" + marid + "&marnm=" + marnm + "&marcz=" + marcz);
        }
        function chaifen(obj) {
            var pattern = new RegExp("[&]"); 
            var shape = "<%=gloabshape%>";
            var mpid = "<%=gloabsheetno%>";
            var ptcode = obj.title;
            var autonum = Math.round(10000 * Math.random());
            window.showModalDialog('PC_Date_chaifen.aspx?autonum=' + autonum + '&mpid=' + mpid + '&ptcode=' + ptcode.replace(pattern,"%26") + '&shape=' + shape + '', '', "dialogHeight:500px;dialogWidth:1200px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + shape + "&mp_id=" + mpid + "";
        }
        function openclosemodewin() {
            var shape = "<%=gloabshape%>";
            var orderno = "<%=gloabsheetno%>";
            var autonum = Math.round(10000 * Math.random());
            //       window.showModalDialog('PC_Date_closemarshow.aspx?autonum='+autonum+'&orderno='+escape(orderno)+'&shape='+escape(shape)+'','',"dialogHeight:500px;dialogWidth:1200px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.open("PC_Date_closemarshow.aspx?autonum=" + autonum + "&shape=" + shape + "&orderno=" + orderno + "");
            window.location.href = "PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + shape + "&mp_id=" + orderno + "";
            //      window.location.reload();
        }

        function openshowallwin() {
            var shape = "<%=gloabshape%>";
            var orderno = "<%=gloabsheetno%>";
            var autonum = Math.round(10000 * Math.random());
            //       window.showModalDialog('PC_Date_closemarshow.aspx?autonum='+autonum+'&orderno='+escape(orderno)+'&shape='+escape(shape)+'','',"dialogHeight:500px;dialogWidth:1200px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.open("PC_TBPC_Purchaseplan_ShowALL.aspx?autonum=" + autonum + "&shape=" + shape + "&orderno=" + orderno + "");
            window.location.href = "PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + shape + "&mp_id=" + orderno + "";
            //      window.location.reload();
        }

        function Showclose(ptcode) {
            var shape = "<%=gloabshape%>";
            var orderno = "<%=gloabsheetno%>";
            var autonum = Math.round(10000 * Math.random());
            window.open("PC_Data_hangclose.aspx?autonum=" + autonum + "&shape=" + shape + "&orderno=" + orderno + "&arry=" + ptcode, 'obj', "dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
            //       window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx";
        }
 
    </script>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 1,
                fixedCols: 0,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    var ptc = "<%=gloabptc%>";
                    if (ptc != "") {
                        var col = 2;
                        var table = document.getElementById("tab");
                        var tr = table.getElementsByTagName("tr");
                        for (i = 1; i < tr.length - 2; i++) {
                            var Qptc = tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                            if (ptc.substring(0, ptc.indexOf("#")) != "") {
                                ptc = ptc.substring(0, ptc.indexOf("#"));
                            }

                            if (Qptc == ptc) {
                                tr[i].style.backgroundColor = 'Orange';
                                //                            tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[1].getElementsByTagName("input")[0].checked=true;
                            }
                            else {
                                tr[i].style.backgroundColor = '';
                            }
                        }
                    }
                },
                onFinish: function() {

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
                            <td align="right" width="44%">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                        Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                                    <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                                    <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                                    <asp:HyperLink ID="addpcpurbill" CssClass="hand" runat="server" BackColor="Red">
                                        驳回计划</asp:HyperLink>&nbsp;&nbsp;
                                    <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="addpcpurbill"
                                        PopupControlID="palPSHTLB" Position="Bottom" OffsetY="4" OffsetX="-65" CacheDynamicResults="false">
                                    </asp:PopupControlExtender>
                                    <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="Btn_daochu" runat="server" Text="导出Excel" OnClick="Btn_daochu_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_showall" runat="server" Text="查看全部" OnClientClick="openshowallwin()" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_CKclose" runat="server" Text="占用/相似代用查看" OnClientClick="openclosemodewin()" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_hclose" runat="server" Text="占用/相似代用" OnClick="btn_CKclose_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_zy" runat="server" Text="库存占用" OnClick="btn_zy_Click" Visible="false" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_rep" runat="server" Text="相似代用" OnClick="btn_rep_Click" Visible="false" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_ph" runat="server" Text="下推" OnClick="btn_ph_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_nosto" runat="server" Text="wukucun" OnClick="btn_nosto_Click" />
                                    <%-- <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />--%>
                                    <asp:Label ID="xinhao" runat="server" Visible="false"></asp:Label>
                                    <a href="javascript:history.go(-1);">向上一页</a> &nbsp;&nbsp;
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="palPSHTLB" Style="visibility: hidden; border-style: solid; border-width: 1px;
                        border-color: blue; background-color: Menu;" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="430px">
                                    <tr>
                                        <td>
                                            <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                    cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                    title="关闭">X</a>
                                            </div>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            驳回意见：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_bhyj" runat="server" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="驳回" />
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
                <table width="100%">
                    <tr>
                        <td align="center" colspan="4">
                            材料需用计划表
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：
                            <asp:TextBox ID="tb_dep" runat="server" Enabled="false"></asp:TextBox>
                            <asp:TextBox ID="tb_depid" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：
                            <asp:TextBox ID="Tb_shijian" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                        </td>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：
                            <asp:TextBox ID="Tb_mpid" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 10%;" align="left">
                            <asp:RadioButton ID="radio_WXT" runat="server" Text="未下推" GroupName="xiatui" OnCheckedChanged="radio_WXT_CheckedChanged"
                                AutoPostBack="True" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;目：
                            <asp:TextBox ID="tb_pjinfo" runat="server" Enabled="false" Text=""></asp:TextBox>
                            <asp:TextBox ID="tb_pj" runat="server" Visible="false" Text=""></asp:TextBox>
                            <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                        </td>
                        <td style="width: 30%;" align="left">
                            &nbsp;&nbsp;&nbsp;设&nbsp;&nbsp;&nbsp;备：
                            <asp:TextBox ID="tb_enginfo" runat="server" Enabled="false" Text="" Width="150px"></asp:TextBox>
                            <asp:TextBox ID="tb_eng" runat="server" Visible="false" Text=""></asp:TextBox>
                            <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                        </td>
                        <td id="Td4" style="width: 30%;" align="left" runat="server">
                            &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                            <asp:TextBox ID="tb_note" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 10%;" align="left">
                            <asp:RadioButton ID="radio_YXT" runat="server" Text="已下推" GroupName="xiatui" OnCheckedChanged="radio_YXT_CheckedChanged"
                                AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
                <div style="height: 390px; overflow: auto; width: 100%">
                    <div class="cpbox3 xscroll">
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="Purchaseplan_startcontentRepeater" runat="server" OnItemDataBound="Purchaseplan_startcontentRepeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong style="color: Red">拆分</strong>
                                        </td>
                                        <td>
                                            <strong>计划号</strong>
                                        </td>
                                        <td>
                                            <strong>图号/标识号</strong>
                                        </td>
                                        <td>
                                            <strong>物料编码</strong>
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
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>类型</strong>
                                        </td>
                                        <td id="length1" runat="server">
                                            <strong>长度</strong>
                                        </td>
                                        <td id="width1" runat="server">
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>需用数量</strong>
                                        </td>
                                        <td>
                                            <strong>单位</strong>
                                        </td>
                                        <td id="fznum1" runat="server">
                                            <strong>需用辅助数量</strong>
                                        </td>
                                        <td id="fzunit1" runat="server">
                                            <strong>辅助单位</strong>
                                        </td>
                                        <td>
                                            <strong>库存数量</strong>
                                        </td>
                                        <td>
                                            <strong>建议采购日期</strong>
                                        </td>
                                        <td>
                                            <strong>采购数量</strong>
                                        </td>
                                        <td id="rpfznum1" runat="server">
                                            <strong>采购辅助数量</strong>
                                        </td>
                                        <td>
                                            <strong>执行人</strong>
                                        </td>
                                        <td>
                                            <strong>申请人</strong>
                                        </td>
                                        <%-- <td id="td0" runat="server" visible="false">
                                                <strong>比价单号</strong>
                                            </td>
                                            <td id="td1" runat="server" visible="false">
                                                <strong>订单号</strong>
                                            </td>--%>
                                        <td>
                                            <strong>行关闭备注</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td runat="server" visible="false">
                                            <strong>计划模式</strong>
                                        </td>
                                        
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" ondblclick="xsuse(this)" onclick="MouseClick(this)">
                                        <td>
                                            <asp:Label ID="Lab_NUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                onclick="checkme(this)"></asp:CheckBox>
                                            <asp:Label ID="PUR_ID" runat="server" Text='<%#Eval("PUR_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" runat="server" onClick="chaifen(this);" ToolTip='<%# Eval("ptcode")%>'
                                                ForeColor="Red">
                                                <asp:Image ID="Image1" ImageUrl="~/Assets/icons/move_arrange.gif" Height="20" Width="20"
                                                    align="absmiddle" runat="server" title="拆分" Style="cursor: pointer" />
                                            </asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                        </td>
                                        <td id="length2" runat="server">
                                            <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                        </td>
                                        <td id="width2" runat="server">
                                            <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_UNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                        </td>
                                        <td id="fznum2" runat="server">
                                            <asp:Label ID="PUR_FZNUM" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                        </td>
                                        <td id="fzunit2" runat="server">
                                            <asp:Label ID="PUR_FZUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="SM_KUCUN" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_TIMEQ" runat="server" Text='<%#Eval("jstimerq")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("rpnum")%>'></asp:Label>
                                        </td>
                                        <td id="rpfznum2" runat="server">
                                            <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("rpfznum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("cgrid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("cgrnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_SQRNM" runat="server" Text='<%#Eval("sqrnm")%>'></asp:Label>
                                        </td>
                                        <%-- <td>
                                                <asp:HyperLink ID="Hypbjd" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_BJD" runat="server" ></asp:Label></asp:HyperLink>
                                            </td>--%>
                                        <%--<td>
                                                <asp:HyperLink ID="Hypdd" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_DD" runat="server" Text='<%#get_pur_dd(Eval("purstate").ToString())%>'></asp:Label></asp:HyperLink>  
                                            </td>--%>
                                        <%-- <td id="td0" runat="server" visible="false">
                                                <asp:Label ID="PIC_SHEETNO" runat="server" Text='<%#Eval("picno")%>'></asp:Label>
                                            </td>
                                            <td id="td1" runat="server" visible="false">
                                                <asp:Label ID="PO_SHEETNO" runat="server" Text='<%#Eval("orderno")%>'></asp:Label>
                                            </td>--%>
                                        <td>
                                            <asp:Label ID="PUR_ZYDY" runat="server" Text='<%#Eval("PUR_ZYDY")%>' ForeColor="Red"></asp:Label>
                                            <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("purstate")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PUR_CSTATE" runat="server" Text='<%#Eval("PUR_CSTATE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("purnote")%>'></asp:Label>
                                        </td>
                                        <td runat="server" visible="false">
                                            <asp:Label ID="Label4" runat="server" Text="MTO"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <td colspan="12" id="lenwid" runat="server">
                                        汇总(RMB/元)
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_prenum" runat="server" Text=""></asp:Label>
                                    </td>
                                     <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td id="ftfznum" runat="server">
                                        <asp:Label ID="lb_fznum" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="ftfzunit" runat="server">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_cgnum" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="ftcgfznum" runat="server">
                                        <asp:Label ID="lb_cgfznum" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td colspan="8">
                                    </td>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="25" align="center">
                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>

                        <script language="javascript" type="text/javascript">
                            var myST = new superTable("tab", {
                                cssSkin: "sDefault",
                                headerRows: 1,
                                fixedCols: 0,
                                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                onStart: function() {
                                    var ptc = "<%=gloabptc%>";
                                    if (ptc != "") {
                                        var col = 2;
                                        var table = document.getElementById("tab");
                                        var tr = table.getElementsByTagName("tr");
                                        for (i = 1; i < tr.length - 2; i++) {
                                            var Qptc = tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML;
                                            if (ptc.substring(0, ptc.indexOf("#")) != "") {
                                                ptc = ptc.substring(0, ptc.indexOf("#"));
                                            }

                                            if (Qptc == ptc) {
                                                tr[i].style.backgroundColor = 'Orange';
                                                //                            tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[1].getElementsByTagName("input")[0].checked=true;
                                            }
                                            else {
                                                tr[i].style.backgroundColor = '';
                                            }
                                        }
                                    }
                                },
                                onFinish: function() {

                                }
                            });
                        </script>

                    </div>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                负责人:<asp:TextBox ID="Tb_fuziren" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="Tb_fuzirenid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                申请人:<asp:TextBox ID="Tb_shenqingren" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="Tb_shenqingrenid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                制单:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
