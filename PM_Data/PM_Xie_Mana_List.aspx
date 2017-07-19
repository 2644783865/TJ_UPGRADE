<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PM_Xie_Mana_List.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_Mana_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产外协比价
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">  
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args)
        {
            
        }
        function EndRequestHandler(sender, args)
        {
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
                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                  
                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                        if (coltext == coltextbef) {
                            for (var k = 1; k <= 3; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        比价状态：
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_Click"
                                            Text="连选" />
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="wxstatus" RepeatColumns="3" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="wxstatus_SelectedIndexChanged">
                                            <asp:ListItem Text="未执行" Value="4" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="已执行" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="变更/取消" Value="5"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        生产外协类别：
                                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                            <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="工序外协" Value="工序外协"></asp:ListItem>
                                            <asp:ListItem Text="成品外协" Value="成品外协"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <%--  <asp:Label ID="Label1" runat="server" Text="按任务号查看："></asp:Label>
                                        <asp:TextBox ID="txtname" runat="server" />
                                        <asp:Button ID="search" runat="server" Text="查看" OnClick="search_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp;--%>
                                        <asp:Button ID="btn_query" runat="server" Text="筛选" Font-Size="Medium" BorderColor="LightGreen"
                                            BorderStyle="Solid" BorderWidth="2px" />
                                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_query"
                                            PopupControlID="select" Y="102" X="600" CancelControlID="guan">
                                        </asp:ModalPopupExtender>
                                        <asp:Button ID="btn_cmpprc" runat="server" Text="询比价" OnClick="btn_cmpprc_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btn_bgcancel" runat="server" Text="变更/取消" OnClick="btn_bgcancel_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
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
                                                text-decoration: none; padding: 5px;" title="关闭">X</a>
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
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt1" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic1" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
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
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt2" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic2" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
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
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt3" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic3" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
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
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt4" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic4" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
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
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt5" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic5" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
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
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">不包含</asp:ListItem>
                                            <asp:ListItem Value="2">等于</asp:ListItem>
                                            <asp:ListItem Value="3">不等于</asp:ListItem>
                                            <asp:ListItem Value="4">大于</asp:ListItem>
                                            <asp:ListItem Value="5">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="6">小于</asp:ListItem>
                                            <asp:ListItem Value="7">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Txt6" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLogic6" BackColor="AliceBlue" runat="server">
                                            <asp:ListItem Value="OR">或者</asp:ListItem>
                                            <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: right">
                                        <asp:Button ID="reset" runat="server" Text="重 置" OnClick="reset_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnfind" runat="server" Text="搜 索" OnClick="search_Click" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnfind" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" align="center" class="nowrap cptable fullwidth">
                                    <asp:Repeater ID="tbpc_otherpurbill_list_Repeater" runat="server">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>批号</strong>
                                                </td>
                                                <td>
                                                    <strong>任务号</strong>
                                                </td>
                                                <td>
                                                    <strong>合同号</strong>
                                                </td>
                                                <td>
                                                    <strong>项目</strong>
                                                </td>
                                                <td>
                                                    <strong>设备</strong>
                                                </td>
                                                <td>
                                                    <strong>外协单号</strong>
                                                </td>
                                                <td>
                                                    <strong>图号</strong>
                                                </td>
                                                <td>
                                                    <strong>总序</strong>
                                                </td>
                                                <td>
                                                    <strong>外协类别</strong>
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
                                                    <strong>数量</strong>
                                                </td>
                                                <td>
                                                    <strong>图纸<br />
                                                        单重<br />
                                                        (kg)</strong>
                                                </td>
                                                <td>
                                                    <strong>图纸<br />
                                                        总重<br />
                                                        (kg)</strong>
                                                </td>
                                                <td>
                                                    <strong>材料<br />
                                                        类别</strong>
                                                </td>
                                                <td>
                                                    <strong>长度</strong>
                                                </td>
                                                <td>
                                                    <strong>宽度</strong>
                                                </td>
                                                <td>
                                                    <strong>下料<br />
                                                        备注</strong>
                                                </td>
                                                <td>
                                                    <strong>下料</strong>
                                                </td>
                                                <td>
                                                    <strong>加工</strong>
                                                </td>
                                                <td>
                                                    <strong>外协备注</strong>
                                                </td>
                                                <td>
                                                    <strong>外形尺寸</strong>
                                                </td>
                                                <td>
                                                    <strong>入库<br />
                                                        类别</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="MS_ID" runat="server" Text='<%#Eval("MS_ID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="MS_CODE" runat="server" Text='<%#Eval("MS_CODE")%>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        onclick="checkme(this)" Checked="false"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PID" runat="server" Text='<%#Eval("MS_PID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_ENGID" runat="server" Text='<%#Eval("MS_ENGID") %>'></asp:Label>
                                                    <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbMS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbCM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <div style="width: 100px; white-space: normal">
                                                        <asp:Label ID="MS_ENGNAME" runat="server" Text='<%#Eval("MS_ENGNAME")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WSID" runat="server" Text='<%#Eval("MS_WSID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUHAO" runat="server" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_ZONGXU" runat="server" Text='<%#Eval("MS_ZONGXU")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_wxtype" runat="server" Text='<%#Eval("MS_wxtype") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <div style="width: 80px; white-space: normal">
                                                        <asp:Label ID="MS_NAME" runat="server" Text='<%#Eval("MS_NAME")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_GUIGE" runat="server" Text='<%#Eval("MS_GUIGE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_CAIZHI" runat="server" Text='<%#Eval("MS_CAIZHI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_NUM" runat="server" Text='<%#Eval("MS_NUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUWGHT" runat="server" Text='<%#Eval("MS_TUWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_TUTOTALWGHT" runat="server" Text='<%#Eval("MS_TUTOTALWGHT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_MASHAPE" runat="server" Text='<%#Eval("MS_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_LEN" runat="server" Text='<%#Eval("MS_LEN")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WIDTH" runat="server" Text='<%#Eval("MS_WIDTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <div style="width: 80px; white-space: normal">
                                                        <asp:Label ID="MS_NOTE" runat="server" Text='<%#Eval("MS_NOTE")%>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_XIALIAO" runat="server" Text='<%#Eval("MS_XIALIAO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_XHBZ" runat="server" Text='<%#Eval("MS_XHBZ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_WAIXINGCHICUN" runat="server" Text='<%#Eval("MS_WAIXINGCHICUN")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="MS_KU" runat="server" Text='<%#Eval("MS_KU")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <div style="width: 100px; white-space: normal">
                                                        <asp:Label ID="MS_ALLBEIZHU" runat="server" Text='<%#Eval("MS_ALLBEIZHU")%>'></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="16" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有记录！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>

                                <script language="javascript" type="text/javascript">   
        var myST = new superTable("tab", {
	        cssSkin : "tDefault",
	        headerRows : 1,
	        fixedCols : 0,
//	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
	        onStart : function () 
	        {
//		        this.start = new Date();
	        },
	        onFinish : function () 
	        {	             
                for (var i=1, j=this.sDataTable.tBodies[0].rows.length-2; i<j; i++) 
                {
                    var dataRow = this.sDataTable.tBodies[0].rows[i+1];
                    var coltextbef=this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    var coltext=this.sDataTable.tBodies[0].rows[i+1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                    if(coltext==coltextbef)
                    {
                          for(var k=1;k<=3;k++)
                          {
                               dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display ="none";  
                          }
                    }
                }
                 
//		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
            }
        });
                                </script>

                            </div>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                没有记录!</asp:Panel>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
