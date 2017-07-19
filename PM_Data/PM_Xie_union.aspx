<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Xie_union.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Xie_union" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产外协管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script type="text/javascript">
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
                            for (var k = 2; k <= 5; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
    </script>

    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                生产外协汇总
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                    AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                外协类型：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_wx" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_wx_OSIC">
                                    <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="工序外协" Value="工序外协"></asp:ListItem>
                                    <asp:ListItem Text="成品外协" Value="成品外协"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                请选择查询类型
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_query" runat="server">
                                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="任务号" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="设备名称" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="批号" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="图号" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="材料类别" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="单号" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_query" runat="server"></asp:TextBox>
                                <asp:Button ID="btn_query" runat="server" Text="查询" OnClick="btn_query_OnClick" />
                            </td>
                            <td>
                                <asp:Button ID="btn_xiacha" runat="server" Text="下查" OnClick="btn_xiacha_OnClick" />
                            </td>
                            <td>
                                <asp:Button ID="btnexport" runat="server" Text="导 出" OnClick="btnexport_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="height: 450px; overflow: auto; width: 100%">
                    <div class="cpbox2 xscroll">
                        <table id="tab" align="center" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="waixie_list_Repeater" runat="server" OnItemDataBound="waixie_list_Repeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                        <td>
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>单号</strong>
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
                                            <strong>设备名称</strong>
                                        </td>
                                        <td>
                                            <strong>批号</strong>
                                        </td>
                                        <td>
                                            <strong>图号</strong>
                                        </td>
                                        <td>
                                            <strong>总序</strong>
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
                                            <strong>图纸单重<br />
                                                （kg）</strong>
                                        </td>
                                        <td>
                                            <strong>图纸总重<br />
                                                （kg）</strong>
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
                                        <%--<td>
                                            <strong>备注</strong>
                                        </td>--%>
                                        <%--<td>
                                            <strong>下料</strong>
                                        </td>--%>
                                        <td>
                                            <strong>加工</strong>
                                        </td>
                                        <td>
                                            <strong>外协备注</strong>
                                        </td>
                                        <td>
                                            <strong>交货期</strong>
                                        </td>
                                        <td>
                                            <strong>外协类别</strong>
                                        </td>
                                        <td>
                                            <strong>审批状态</strong>
                                        </td>
                                        <td runat="server" id="hedit">
                                            <strong>修改</strong>
                                        </td>
                                        <td runat="server" id="hlookup">
                                            <strong>查看</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <asp:CheckBox ID="CKBOX_SELECT" runat="server" Width="8px" CssClass="checkBoxCss" />
                                        </td>
                                        <td>
                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:Label ID="MS_ID" Visible="false" runat="server" Text='<%#Eval("MS_ID")%>'></asp:Label>
                                            <asp:Label ID="MS_scwaixie" Visible="false" runat="server" Text='<%#Eval("MS_scwaixie")%>'></asp:Label>
                                            <asp:Label ID="PM_SUBMITID" Visible="false" runat="server" Text='<%#Eval("PM_SUBMITID")%>'></asp:Label>
                                            <asp:Label ID="MS_CODE" Visible="false" runat="server" Text='<%#Eval("MS_CODE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PM_DocuNum" runat="server" Text='<%#Eval("PM_DocuNum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MS_ENGID" runat="server" Text='<%#Eval("MS_ENGID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbMS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                        </td>
                                        <td>
                                           <%-- <div style="width: 100px; white-space: normal">--%>
                                                <asp:Label ID="MS_ENGNAME" runat="server" Text='<%#Eval("MS_ENGNAME")%>'></asp:Label>
                                                <%--<%#Eval("MS_ENGNAME")%>--%>
                                   <%--         </div>--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="MS_PID" runat="server" Text='<%#Eval("MS_PID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MS_TUHAO" runat="server" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MS_ZONGXU" runat="server" Text='<%#Eval("MS_ZONGXU")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 70px; white-space: normal">
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
                                        <%-- <td>
                                            <asp:Label ID="MS_NOTE" runat="server" Text='<%#Eval("MS_NOTE")%>'></asp:Label> 
                                               </td>
                                                   <td>
                                            <asp:Label ID="MS_XIALIAO" runat="server" Text='<%#Eval("MS_XIALIAO")%>'></asp:Label> 
                                                 </td>--%>
                                        <td>
                                            <asp:Label ID="MS_PROCESS" runat="server" Text='<%#Eval("MS_PROCESS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="MS_WAIXINGCHICUN" runat="server" Text='<%#Eval("MS_WAIXINGCHICUN")%>'></asp:Label>--%>
                                            <asp:Label ID="MS_XHBZ" runat="server" Text='<%#Eval("MS_XHBZ")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PM_JHQ" runat="server" Text='<%#Eval("PM_JHQ")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MS_wxtype" runat="server" Text='<%#Eval("MS_wxtype")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PM_SPZT" runat="server" Text='<%#Eval("PM_SPZT")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="PM_SPZTTEXT" runat="server" Text='<%#get_spzt(Eval("PM_SPZT").ToString())%>'></asp:Label>
                                        </td>
                                        <td runat="server" id="bedit">
                                            <asp:HyperLink ID="hyp_edit" runat="server">
                                                <asp:Label ID="Label1" runat="server" Text="修改"></asp:Label></asp:HyperLink>
                                        </td>
                                        <td runat="server" id="blookup">
                                            <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                                <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
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
                                                    for (var k = 2; k <= 5; k++) {
                                                        dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
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
            <div>
                <table width="100%">
                    <tr>
                        <td width="50%" style="text-align: center">
                            共查出<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>批计划单/
                            <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red"></asp:Label>条记录
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
