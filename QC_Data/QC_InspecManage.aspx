<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_InspecManage.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_InspecManage"
    Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">        //在GridView只选中一个CheckBox
        var AFI_ID;
        function ShowUpdateModal(ID) {
            AFI_ID = ID;
            SelPersons();
            //     
            //        var frame = $get('IframeView');
            //        frame.width=getWidth();
            //        frame.height=getHeight();  
            //        frame.src = "QC_InspecTaskAssign.aspx?id="+ID;
            //        $find('ViewModalPopup').show();

        }
        function savePick() {
            var a = Save();
            $.ajax({
                type: "POST",
                url: "QC_AjaxHandler.aspx",
                data: "AFI_ID=" + AFI_ID + "&method=InspecAssign&perID=" + a.st_id + "&perName=" + a.st_name,
                success: function(msg) {
                    $.messager.show({
                        title: '提示消息',
                        msg: msg,
                        timeout: 5000
                    });
                }
            });
            $('#win').dialog('close');
        }

        function ViewOkayScript() {
            //在这里可以写相应的js事件
            RefreshDataGrid();
        }

        function RefreshDataGrid() {
            document.getElementById("<%=ReloadGrid.ClientID%>").click();
        }

        //计算遮罩层的高
        function getHeight() {
            var winWidth
            if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
                winHeight = document.documentElement.scrollHeight;
            }
            else {
                winHeight = document.documentElement.clientHeight;
            }
            if (navigator.appName !== "Microsoft Internet Explorer") {
                winHeight = winHeight - 4;
            }
            else {
                winHeight = winHeight;
            }
            return winHeight;
        }

        //计算遮罩层的宽
        function getWidth() {
            var winWidth;
            if (document.documentElement.scrollWidth > document.documentElement.clientWidth) {
                winWidth = document.documentElement.scrollWidth;
            }
            else {
                winWidth = document.documentElement.clientWidth;
            }
            if (navigator.appName !== "Microsoft Internet Explorer") {
                winWidth = winWidth - 4;
            }
            else {
                winWidth = winWidth;
            }
            return winWidth;
        }

        function ShowViewModal(ID) {
            var date = new Date();
            var time = date.getTime();
            var retVal = window.open("QC_Inspection_Add.aspx?ACTION=VIEW&&back=1&&id=" + ID);
        }
    
    </script>

    <asp:Button ID="ButtonEdit" runat="server" Text="查看" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtenderEdit" runat="server" CancelControlID="ButtonViewCancel"
        OkControlID="ButtonViewDone" TargetControlID="ButtonEdit" PopupControlID="DivEViewWindow"
        Drag="True" PopupDragHandleControlID="DivEViewWindowHandle" Enabled="True" OnOkScript="ViewOkayScript();"
        BehaviorID="ViewModalPopup">
    </asp:ModalPopupExtender>
    <div style="display: none">
        <input id="ButtonViewDone" value="Done" type="button" />
        <input id="ButtonViewCancel" value="Cancel" type="button" />
    </div>
    <div id="DivEViewWindowHandle">
        <div id="DivEViewWindow" style="display: none; background-color: #A8B7EC;">
            <iframe id="IframeView" frameborder="0" scrolling="no"></iframe>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 98%; height: 24px">
                    <tr>
                        <td align="right" width="10%">
                            状态:
                        </td>
                        <td width="15%">
                            <asp:RadioButtonList ID="rblstatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="0" Selected="True">未分工</asp:ListItem>
                                <asp:ListItem Value="1">已分工</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="10%" align="right">
                            <asp:Label ID="result" runat="server" Text="质检结果:" Visible="true"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:RadioButtonList ID="rblresult" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                AutoPostBack="True" Visible="true">
                                <asp:ListItem Value="0">未质检</asp:ListItem>
                                <asp:ListItem Value="1">合格</asp:ListItem>
                                <asp:ListItem Value="2">不合格</asp:ListItem>
                                <asp:ListItem Value="3">关闭</asp:ListItem>
                                <asp:ListItem Value="4">全部</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="10%" align="right">
                            <asp:DropDownList ID="DropDownListType" runat="server" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="AFI_TSDEP">报检部门</asp:ListItem>
                                <asp:ListItem Value="AFI_RQSTCDATE" Selected="True">检测时间</asp:ListItem>
                                <asp:ListItem Value="AFI_ENDDATE">质检时间</asp:ListItem>
                                <asp:ListItem Value="AFI_ENDRESLUT">质检结果</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="12%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListOrderBy" runat="server" RepeatDirection="Horizontal"
                                BorderStyle="None" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="1" Selected="True">降序</asp:ListItem>
                                <asp:ListItem Value="0">升序</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" >
                           
                        </td>
                        <td align="left" >
                         <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                         &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnOutPutExcel" runat="server" OnClick="btnOutPutExcel_Click" Text="导出" />
                             &nbsp; &nbsp; &nbsp;
                              <asp:HyperLink runat="server" ID="hplInDetail" NavigateUrl="~/QC_Data/QC_InspectDetail.aspx">质检明细</asp:HyperLink>
                        </td>
                
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="padding-right: 8px; padding-left: 8px">
            <div style="width: 100%; padding-bottom: 20px;">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项目名称：<asp:TextBox ID="TextBoxProj" runat="server"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="xmmc_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                            Enabled="True" FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="xmmc"
                                            ServicePath="~/Ajax.asmx" TargetControlID="TextBoxProj" UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        报检部门：
                                        <asp:DropDownList ID="DropDownListDep" runat="server">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="06">采购部</asp:ListItem>
                                                <asp:ListItem Value="07">市场部</asp:ListItem>
                                            <asp:ListItem Value="03">技术质量部</asp:ListItem>
                                            <asp:ListItem Value="04">生产管理部</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;报检人：<asp:TextBox ID="TextBoxInspecMan" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;质检人：<asp:TextBox ID="TextBoxMan" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;质检编号：<asp:TextBox ID="TextBoxZJID" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;设备名称：<asp:TextBox ID="TextBoxEng" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        部件名称：<asp:TextBox ID="TextBoxPart" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;任务号：<asp:TextBox ID="TextBoxENGID" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;供货商：<asp:TextBox ID="TextBoxSUPPLERNM" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;合同号：<asp:TextBox ID="TextBoxheT" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        &nbsp;&nbsp;&nbsp;质检时间 从：<asp:TextBox ID="txtStartTime" runat="server" class="easyui-datebox"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap; width: 20%;" align="left">
                                        到：<asp:TextBox ID="txtEndTime" runat="server" class="easyui-datebox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" OnDataBound="GridView1_DataBound" OnRowDataBound="GridView1_RowDataBound"
                            DataKeyNames="AFI_ID">
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" Height="21px" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="质检编号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbafiid" runat="server" Text='<%# Eval("AFI_ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbafisj" runat="server" Text='<%# Eval("AFI_DATE") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbzjid" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TSA_PJID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_ENGID" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_PJNAME" HeaderText="项目名称" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_ENGNAME" HeaderText="设备名称" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_PARTNAME" HeaderText="部件名称" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_TSDEP" HeaderText="报检部门" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_MANNM" HeaderText="报检人" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_RQSTCDATE" HeaderText="需要检测时间" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="AFI_ISPCTSITE" HeaderText="检测地点" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="报检次数">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_state" runat="server" Text='<%#Eval("AFI_NUMBER")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AFI_QCMANNM" HeaderText="质检人" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" HeaderText="质检结果">
                                    <ItemTemplate>
                                        <asp:Label ID="lb_result" runat="server" ForeColor='<%#Eval("AFI_ENDRESLUT").ToString()=="不合格"?System.Drawing.Color.Red:Eval("AFI_ENDRESLUT").ToString()=="合格"?System.Drawing.Color.Blue:Eval("AFI_ENDRESLUT").ToString()=="关闭"?System.Drawing.Color.Green:System.Drawing.Color.White%>'
                                            Text='<%#Eval("AFI_ENDRESLUT")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AFI_ENDDATE" HeaderText="质检时间" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="任务分工" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hytask" runat="server">
                                            <asp:Image ID="imgtask" ImageUrl="~/assets/images/res.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            任务分工</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="详细信息" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hydetail" runat="server">
                                            <asp:Image ID="imgdetail" ImageUrl="~/assets/images/res.gif" border="0" hspace="2"
                                                align="absmiddle" runat="server" />
                                            详细信息</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                            没有任务!</asp:Panel>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ReloadGrid" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="ReloadGrid" runat="server" Text="ReloadGrid" OnClick="ReloadGrid_Click"
                    Style="display: none" />
            </div>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
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
                        <input id="dep" name="dept" value="12">
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
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a>
    </div>
</asp:Content>
