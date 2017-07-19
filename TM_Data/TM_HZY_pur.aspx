<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_HZY_pur.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_HZY_pur" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划表
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
            <JSR:JSRegister ID="JSRegister1" runat="server" />
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="98%">
                            <tr>
                                <td>
                                    任务号：
                                    <asp:Label ID="tsa_id" runat="server"></asp:Label>
                                </td>
                                <td>
                                    合同号：
                                    <asp:Label ID="lab_contract" runat="server"></asp:Label>
                                    <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                    <input id="mp_no" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                    <input id="status" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                                </td>
                                <td>
                                    项目名称：
                                    <asp:Label ID="lab_proname" runat="server"></asp:Label>
                                </td>
                                <td>
                                    设备名称：
                                    <asp:Label ID="lab_engname" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <div runat="server" id="div_show_zero">
                                        <asp:CheckBox ID="ckbShowZero" runat="server" OnCheckedChanged="rad_detail_CheckedChanged"
                                            AutoPostBack="true" />显示不可提交计划项</div>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_concel" runat="server" Text="取 消" OnClick="btn_concel_Click"
                                        Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btn_gosh" runat="server" Text="gosh" OnClick="btn_gosh_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCheck" runat="server" Text="审核" Visible="false" OnClientClick="return confirm('确认下推审核吗？');"
                                        OnClick="btn_gosh_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelAll" runat="server" Text="全部取消" OnClick="btnCancelAll_OnClick"
                                        OnClientClick="return confirm('确认要全部取消吗？');" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%">
                        <tr>
                            <td rowspan="2">
                                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged"
                                    Text="全选" />
                            </td>
                            <td align="left">
                                材料名称:<asp:DropDownList ID="ddlMarName" runat="server" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                材料规格:<asp:DropDownList ID="ddlMarGuiGe" runat="server" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                材质:<asp:DropDownList ID="ddlMarCaiZhi" runat="server" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server">
                                    <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                                <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                    Position="Left" Enabled="true" runat="server" OffsetX="-500" OffsetY="0" TargetControlID="HyperLink1"
                                    PopupControlID="palORG">
                                </cc1:PopupControlExtender>
                                <asp:Panel ID="palORG" Width="50%" Style="display: none; visibility: hidden; border-style: solid;
                                    border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
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
                                                    <td align="right">
                                                        <asp:Button ID="btn_Query" runat="server" UseSubmitBehavior="false" Text="查 询" OnClick="ddlmpName_SelectedIndexChanged" />&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnClear" runat="server" UseSubmitBehavior="false" Text="清 空" OnClick="btnClear_OnClick" />&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <uc3:UserDefinedQueryConditions ID="udqOrg" runat="server" QueryColumnsType="MP"
                                                            QueryRows="4" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                材料类别:
                                <asp:DropDownList ID="ddlmpName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmpName_SelectedIndexChanged">
                                    <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="钢材" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="标准件" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="铸锻件" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="非金属" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="采购成品" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                详细/汇总:
                                <asp:RadioButton ID="rad_detail" runat="server" Text="详细信息" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_detail_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                                <asp:RadioButton ID="rad_sum" runat="server" Text="汇总信息" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_sum_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td align="left">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                        <hr style="width: 100%; height: 0.1px; color: Blue;" />
                        没有记录！！！</asp:Panel>
                    <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                        CellPadding="4" ForeColor="#333333" Width="100%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="MP_NEWXUHAO" HeaderText="序号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:BoundField DataField="MP_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="图号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTuhao" runat="server" Text='<%#Eval("MP_TUHAO")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MP_MARID" HeaderText="物料编码" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="材料种类" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lbshape1" runat="server" Text='<%#Eval("MP_MASHAPE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_LENGTH" HeaderText="长度(mm)" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_WIDTH" HeaderText="宽度(mm)" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_TECHUNIT" HeaderText="单位" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_YONGLIANG" HeaderText="材料用量" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_WEIGHT" DataFormatString="{0:N2}" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" HeaderText="重量(kg)" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("MP_FIXEDSIZE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MP_KU" HeaderText="库" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBeiZhu" runat="server" Text='<%#Eval("MP_ALLBEIZHU")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MP_TRACKNUM" HeaderText="计划跟踪号" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView>
                    <yyc:SmartGridView ID="GridView2" Width="100%" CssClass="toptable grid" runat="server"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                        Visible="false">
                        <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="Index" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="图号" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                
                                    <asp:Label runat="server" Text='<%#Eval("MP_TUHAO") %>' ID="lblTuhao"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="物料编码" DataField="MP_MARID" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="材料名称" DataField="MP_NAME" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="规格" DataField="MP_GUIGE" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="材质" DataField="MP_CAIZHI" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="长度(mm)" DataField="MP_LENGTH" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="宽度(mm)" DataField="MP_WIDTH" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="单位" DataField="MP_TECHUNIT" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="材料用量" DataField="MP_YONGLIANG" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="重量" DataField="MP_WEIGHT" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="标准" DataField="MP_STANDARD" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="Lbshape" runat="server" Text='<%#Eval("MP_MASHAPE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblsfdc2" runat="server" Text='<%#Eval("MP_FIXEDSIZE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <input type="text" runat="server" id="txtBeiZhu" value='<%#Eval("MP_ALLBEIZHU")%>' />
                                    <input type="hidden" runat="server" id="hidNote" value='<%#Eval("MP_ALLBEIZHU")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MP_KU" HeaderText="库" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_TRACKNUM" HeaderText="" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle Wrap="false" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager" TableHeight="500px" TableWidth="100%" FixColumns="0,1" />
                    </yyc:SmartGridView>
                    <uc1:UCPaging ID="UCPagingMS" runat="server" />
                </div>
            </div>
  

</asp:Content>
