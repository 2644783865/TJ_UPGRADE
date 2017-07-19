<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="TM_Design_Bom.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Design_Bom" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    原始数据查询</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner" style="vertical-align: top">
                <div class="box_right">
                    <div class="box-title">
                        <table width="98%">
                            <tr>
                                <td style="width: 16%;" align="left">
                                    <b>产品设计BOM</b>
                                </td>
                                <td align="center">
                                    <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server">
                                        <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />筛选</asp:HyperLink>
                                    <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                        Position="Bottom" Enabled="true" runat="server" OffsetX="-330" OffsetY="0" TargetControlID="HyperLink1"
                                        PopupControlID="palORG">
                                    </asp:PopupControlExtender>
                                    <asp:Panel ID="palORG" Style="display: none; visibility: hidden; border-style: solid;
                                        border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                        <table width="100%">
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
                                                <td colspan="4">
                                                    <div style="text-align: left;">
                                                        <strong>常规查询</strong></div>
                                                    <hr style="height: 2px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="notbrk">
                                                    合同号:
                                                </td>
                                                <td style="width: 24%" align="left">
                                                    <asp:TextBox ID="txtPJId" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    任务号:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtEngId" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="notbrk">
                                                    名称:
                                                </td>
                                                <td style="width: 24%" align="left">
                                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    材料规格:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGuige" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    材料类别:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlOrgState" runat="server" AutoPostBack="true">
                                                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="板" Value="板"></asp:ListItem>
                                                        <asp:ListItem Text="型" Value="型"></asp:ListItem>
                                                        <asp:ListItem Text="非" Value="非"></asp:ListItem>
                                                        <asp:ListItem Text="采" Value="采"></asp:ListItem>
                                                        <asp:ListItem Text="锻" Value="锻"></asp:ListItem>
                                                        <asp:ListItem Text="铸" Value="铸"></asp:ListItem>
                                                        <asp:ListItem Text="采购成品" Value="采购成品"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" class="notbrk">
                                                    图号:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTuhao" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <div style="text-align: left;">
                                                        <strong>自定义查询</strong></div>
                                                    <hr style="height: 2px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    查询条件:
                                                </td>
                                                <td colspan="3" align="left">
                                                    <asp:DropDownList ID="ddlQueryType" runat="server">
                                                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="总序" Value="BM_ZONGXU"></asp:ListItem>
                                                        <asp:ListItem Text="中文名称" Value="BM_CHANAME"></asp:ListItem>
                                                        <asp:ListItem Text="图号" Value="BM_TUHAO"></asp:ListItem>
                                                        <asp:ListItem Text="物料编码" Value="BM_MARID"></asp:ListItem>
                                                        <asp:ListItem Text="规格" Value="BM_GUIGE"></asp:ListItem>
                                                        <asp:ListItem Text="材质" Value="BM_MAQUALITY"></asp:ListItem>
                                                        <asp:ListItem Text="备注" Value="BM_NOTE"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtQueryContent" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                                    <asp:Button ID="btnQuery" runat="server" Text="查询" UseSubmitBehavior="false" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_OnClick" UseSubmitBehavior="false"
                                                        Text="重置" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </asp:Panel>
                                </td>
                                <td>
                                    <asp:Button ID="btn_xiatui" runat="server" Text="下推台账" OnClick="btn_xiatui_OnClick"
                                        Visible="false" />
                                    <asp:LinkButton ID="lnkBtnExport" OnClientClick='return confirm("确认导出吗？\r\r提示:导出时请选择项目、工程！！！")'
                                        OnClick="lnkBtnExport_OnClick" runat="server">
                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />导出EXCEL</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <asp:Panel ID="NoDataPanel" Width="100%" runat="server">
                        <div style="text-align: center; font-size: medium;">
                            <br />
                            没有记录!</div>
                    </asp:Panel>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete()" CssClass="checkBoxCss" />
                                    <input id="Hidden1" type="hidden" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                    <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_MARID").ToString()+"-"+Eval("BM_WMARPLAN").ToString()+"-"+Eval("BM_ISMANU").ToString() %>' />
                                    <%--材料计划状态、材料变更状态、明细状态、明细变更状态、物料编码、是否提材料计划--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center" />
                            <%--<asp:BoundField DataField="BM_XUHAO" HeaderText="序号" HeaderStyle-Wrap="false" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>--%>
                            <asp:BoundField DataField="BM_ENGID" HeaderText="任务号" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TUHAO" HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_CHANAME" HeaderText="中文名称" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量|计划数量" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TUUNITWGHT" DataFormatString="{0:F2}" HeaderText="图纸单重(kg)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TUTOTALWGHT" DataFormatString="{0:F2}" HeaderText="图纸总重(kg)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TECHUNIT" HeaderText="技术单位" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MABGZMY" DataFormatString="{0:F2}" HeaderText="面域(m2)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="计划面域(m2)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="材料总长(mm)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重(kg)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重(kg)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAWIDTH" DataFormatString="{0:F2}" HeaderText="宽度(mm)"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_NOTE" HeaderText="下料备注" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_XIALIAO" HeaderText="下料方式" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_KU" HeaderText="库" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_THRYWGHT" DataFormatString="{0:F2}" HeaderText="理论重量"
                                HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_ISMANU" HeaderText="制作明细" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_WMARPLAN" HeaderText="材料计划" HeaderStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_FILLMANNAME" HeaderText="初始录入" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                        </Columns>
                        <PagerStyle CssClass="bomcolor" Wrap="false" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle Wrap="false" BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <uc1:UCPaging ID="UCPaging1" Visible="false" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 50%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
