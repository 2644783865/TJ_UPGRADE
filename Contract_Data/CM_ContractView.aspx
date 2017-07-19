<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="CM_ContractView.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_ContractView" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <table width="100%">
        <tr>
            <td>
                合同审批
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">
        function RowClick(obj) {
            var table = obj.parentNode.parentNode;
            var tr = table.getElementsByTagName("tr");

            var ss = tr.length;
            for (var i = 1; i < ss - 1; i++) {
                tr[i].style.backgroundColor = (tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
                //       tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
            }
            obj.style.backgroundColor = (obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
            //   obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
        } 
    </script>

    <script type="text/javascript" language="javascript">
        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
        }
    </script>

    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <div class="RightContent">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ToolkitScriptManager>
        <div class="box-wrapper">
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="left" width="25%">
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    <asp:RadioButtonList ID="rblZT" AutoPostBack="true" OnSelectedIndexChanged="Btn_Query_click"
                                        runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="全部" Value="%" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="已审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                        <%--<asp:ListItem Text="未提交" Value="0"></asp:ListItem>--%>
                                        <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dplHTLB" runat="server" Visible="false" AutoPostBack="true"
                                        OnSelectedIndexChanged="Btn_Query_click">
                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="销售合同" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="委外合同" Value="1"></asp:ListItem>
                                        <%--<asp:ListItem Text="厂内分包" Value="2"></asp:ListItem>--%>
                                        <asp:ListItem Text="采购合同" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="办公合同" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="其他合同" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="补充协议" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 制单人:&nbsp;
                                    <asp:DropDownList ID="ddlZDR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Btn_Query_click">
                                        <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server" Style="cursor: pointer">
                                        <asp:Image ID="Image4" ImageUrl="../Assets/icon-fuction/388.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />更多筛选</asp:HyperLink>
                                    <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                        Position="Bottom" Enabled="true" runat="server" OffsetX="50" OffsetY="-80" TargetControlID="HyperLink1"
                                        PopupControlID="palORG">
                                    </asp:PopupControlExtender>
                                    <asp:Panel ID="palORG" Width="340px" Style="display: none; visibility: hidden; border-style: solid;
                                        border-width: 1px; border-color: blue; background-color: white;" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                    font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                    <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                        cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                        title="关闭">X</a>
                                                </div>
                                                <br />
                                                <br />
                                                <table style="width: 100%; background-color: White;">
                                                    <tr>
                                                        <td style="width: 60px" align="right">
                                                            <strong>合同号:</strong>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_htbh" runat="server" Width="260px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <strong>项目名称:</strong>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_xmmc" runat="server" Width="260px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <strong>制单日期:</strong>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtStartTime" runat="server" Width="116px" onchange="dateCheck(this)"></asp:TextBox>
                                                            至&nbsp;<asp:TextBox ID="txtEndTime" runat="server" Width="116px" onchange="dateCheck(this)"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                                                TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtStartTime">
                                                            </asp:CalendarExtender>
                                                            <asp:CalendarExtender ID="calender_end" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                                                TodaysDateFormat="yyyy年MM月dd日" TargetControlID="txtEndTime">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="4">
                                                            <asp:Button ID="Btn_Query" runat="server" Text="查 询" OnClick="Btn_Query_click" />
                                                            &nbsp;&nbsp;
                                                            <asp:Button ID="Btn_Reset" runat="server" Text="重 置" OnClick="Btn_Reset_click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </td>
                                <td align="right" width="40%">
                                    <asp:Button ID="btndaochu" runat="server" Text="导出合同" OnClick="btndaochu_click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:HyperLink ID="hylAddContractView" CssClass="hand" runat="server">
                                        <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />添加合同审批</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="hylAddContractView"
                                        PopupControlID="palPSHTLB" Position="Bottom" OffsetY="-50" OffsetX="-180">
                                    </asp:PopupControlExtender>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="palPSHTLB" Width="230px" Style="visibility: hidden; border-style: solid;
                            border-width: 1px; border-color: blue; background-color: White;" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                        font-size: 11px; font-weight: bold; position: absolute; top: 5px; right: 10px;">
                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                            title="关闭">X</a>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <table style="width: 100%; background-color: White;">
                                        <tr>
                                            <td align="left">
                                                审批合同类别:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dplPSHTLB_Select" runat="server">
                                                    <%--<asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>--%>
                                                    <asp:ListItem Text="销售合同" Value="0" Selected="True"></asp:ListItem>
                                                    <%-- <asp:ListItem Text="生产外协" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="采购合同" Value="3"></asp:ListItem>--%>
                                                    <%-- <asp:ListItem Text="厂内分包" Value="3"></asp:ListItem>--%>
                                                    <%--<asp:ListItem Text="发运合同" Value="3"></asp:ListItem>--%>
                                                    <%-- <asp:ListItem Text="办公合同" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="其他合同" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="补充协议" Value="6"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_OnClick" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="height: 5px">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; position: relative;
                        margin: 2px" Width="100%">
                        <yyc:SmartGridView ID="grvSP" runat="server" ShowFooter="true" Width="100%" AutoGenerateColumns="False"
                            CellPadding="4" OnRowDataBound="grvSP_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1)%>'></asp:Label>
                                        <asp:Label ID="lblCR_ID" runat="server" Text='<%#Eval("CR_ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblZDR" runat="server" Text='<%#Eval("CR_ZDR") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblERCIPS" runat="server" Text='<%#Eval("CR_ERCIPS") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="合同编号" ItemStyle-Wrap="false" DataField="PCON_BCODE" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="项目名称" ItemStyle-Wrap="false" DataField="CR_XMMC" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <%--<asp:BoundField HeaderText="设备名称" ItemStyle-Wrap="false"  DataField="CR_SBMC" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>--%>
                                <asp:BoundField HeaderText="业主名称" ItemStyle-Wrap="false" DataField="CR_FBSMC" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="净利润" ItemStyle-Wrap="false" DataField="CM_JLR" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="净利率" ItemStyle-Wrap="false" DataField="CM_JLL" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <%--<asp:BoundField HeaderText="工程分包范围" ItemStyle-Wrap="false"  DataField="CR_FBFW" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>--%>
                                <asp:BoundField HeaderText="制单人" ItemStyle-Wrap="false" DataField="CR_ZDRNAME" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="制单日期" ItemStyle-Wrap="false" DataField="CR_ZDRQ" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Wrap="false" />
                                <%--<asp:BoundField HeaderText="金额" ItemStyle-Wrap="false"  DataFormatString="{0:c}" DataField="CR_JIN" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false"/>--%>
                                <asp:TemplateField HeaderText="审批状态" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_pszt" runat="server" Text='<%#Eval("CR_PSZT").ToString()=="0"?"未提交":Eval("CR_PSZT").ToString()=="1"?"审批中":Eval("CR_PSZT").ToString()=="2"?"审批完毕":"已驳回" %>'
                                            ForeColor='<%#Eval("CR_PSZT").ToString()=="3"?System.Drawing.Color.Red:System.Drawing.Color.Green %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="合同类别" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbl_htlx" runat="server" Text='<%#Eval("CR_HTLX").ToString()=="0"?"销售合同":Eval("CR_HTLX").ToString()=="1"?"委外合同":Eval("CR_HTLX").ToString()=="2"?"厂内分包":Eval("CR_HTLX").ToString()=="3"?"采购合同":Eval("CR_HTLX").ToString()=="4"?"办公合同":Eval("CR_HTLX").ToString()=="5"?"其他合同":"补充协议" %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text="备注..." ToolTip='<%#Eval("CR_NOTE").ToString()==""?"无备注":Eval("CR_NOTE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn_Edit" runat="server" CommandArgument='<%#Eval("CR_ID") %>'
                                            OnClick="lnkAction_OnClick" CommandName='<%#Eval("CR_HTLX").ToString()+"|"+"edit"%>'
                                            OnClientClick="javascript:return confirm('修改后将清空历史\r审批记录重新开始审批！\r\r继续修改吗？');">
                                            <asp:Image ID="ImageEdit" ImageUrl="~/Assets/images/modify.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn_Del" runat="server" ForeColor="Red" CommandArgument='<%#Eval("CR_ID") %>'
                                            OnClick="Lbtn_Del_OnClick" CommandName='<%#Eval("CR_HTLX") %>' OnClientClick="javascript:return confirm('此操作将同时删除合同审批与合同登记！\r\r确定要删除吗？');">
                                            <asp:Image ID="ImageVoid" ImageUrl="~/Assets/images/erase.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtn_View" runat="server" CommandArgument='<%#Eval("CR_ID") %>'
                                            OnClick="lnkAction_OnClick" CommandName='<%#Eval("CR_HTLX").ToString()+"|"+"view" %>'>
                                            <asp:Image ID="Imageview" ImageUrl="~/Assets/images/search.gif" runat="server" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <RowStyle BackColor="#EFF3FB" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2" />
                        </yyc:SmartGridView>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                            没有记录!</asp:Panel>
                    </asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
