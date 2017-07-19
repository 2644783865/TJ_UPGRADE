<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Packing_List.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Packing_List" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    PACKING LIST(装箱单)     
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="box-wrapper">
    <div style="height:6px" class="box_top"></div>
    <div class="box-outer">
        <table width="98%">
        <tr>
            <td style="width:16%" align="right">General Contractor(承包单位):</td>
            <td colspan="3"><input id="Gen_Contractor" type="text" value="" runat="server" style="width:91%"/></td>
            <td style="width:10%" align="right">Owner(业主):</td>
            <td colspan="3"><input id="Owner" type="text" value="" runat="server" style="width:80%"/></td> 
        </tr>
        <tr>
            <td align="right">Vehicle or Ship No.(船号):</td>
            <td><input id="Ship_No" type="text" runat="server"/></td>
            <td align="right">Consignment Date(发货日期):</td>
            <td><input id="Con_Date" type="text" runat="server" readonly="readonly" value="" onclick="setday(this)"/></td>
            <td align="right">Page of(页数):</td>
            <td><input id="Page" type="text" runat="server" style="width:50px"/></td>
            <td align="right">Signature(签字):</td>
            <td><input id="Signature" type="text" runat="server" style="width:100px"/></td>
        </tr>
        <tr>
            <td align="right">Systerm No.(系统号):</td>
            <td><input id="Sys_No" type="text" runat="server"/></td>
            <td align="right">Name of Goods(货物名称):</td>
            <td><input id="Goods_Name" runat="server" type="text"/></td>
            <td align="right" colspan="4">
                <asp:Button ID="btninsert" runat="server" Text="插 入" OnClientClick="return insert()" 
                    onclick="btninsert_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Text="删 除" OnClientClick="return check()" 
                    onclick="btndelete_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnsave" runat="server" Text="保 存" onclick="btnsave_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"/>
            </td>
        </tr>
    </table>
 </div>

 <div class="box-outer">
        <asp:Panel ID="Panel1" runat="server"  style="height: 420px; width: 100%; position:static">
            <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333">
            
                <%--<asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333">--%>
                   <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="序号" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lblIndex" runat="server" Text='<%# Eval("PL_NO") %>' Width="30px" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="箱号" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="case_no" runat="server" Width="120px" Text='<%# Eval("PL_PACKAGENO") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="设备号"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_Itemno" runat="server" Width="50px" Text='<%# Eval("PL_ITEMNO") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="包装名称" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_name" runat="server" Width="70px" Text='<%# Eval("PL_PACKNAME") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="标识号" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_biaozhi" runat="server" Width="120px" Text='<%# Eval("PL_MARKINGNO") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="包装数量" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_num" runat="server" Width="50px" Text='<%# Eval("PL_PACKQLTY") %>' BorderStyle="None"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="包装类型" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_style" runat="server" Width="50px" Text='<%# Eval("PL_PACKTYPE") %>' BorderStyle="None"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="长(cm)" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_long" runat="server" Width="50px" onblur="setClf(1)" Text='<%# Eval("PL_PACKDIML") %>' BorderStyle="None"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                    <asp:TemplateField HeaderText="宽(cm)" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_width" runat="server" Width="50px" onblur="setClf(2)" Text='<%# Eval("PL_PACKDIMW") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="高(cm)" ItemStyle-Width="45px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_height" runat="server" Width="45px" onblur="setClf(3)" Text='<%# Eval("PL_PACKDIMH") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="体积(m3)" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_volume" runat="server" Width="60px" Text='<%# Eval("PL_TOTALVOLUME") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="单净重" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_netweight" runat="server" Width="80px" Text='<%# Eval("PL_SINGLENETWGHT") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="单毛重" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_grossweight" runat="server" Width="80px" Text='<%# Eval("PL_SINGLEGROSSWGHT") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总毛重" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_totalweight" runat="server" Width="70px" Text='<%# Eval("PL_TOTALGROSSWGHT") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="原因陈述" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_cause" runat="server" Width="120px" Text='<%# Eval("PL_DESCRIPTION") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="外形图" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txt_view" runat="server" Width="70px" Text='<%# Eval("PL_OUTLINEDRAWING") %>' BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                    <FixRowColumn FixRowType="Header,Pager" TableHeight="420px" TableWidth="100%" FixColumns="0,1" />
                <%--</asp:GridView>--%>
                </yyc:SmartGridView>
            </asp:Panel>
            <input id="pk_no" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            <input id="status" type="text" value="" runat="server" readonly="readonly" style="display: none" />
            <input id="txtid" type="text"  runat="server" readonly="readonly" style="display: none" />
            <input id="istid" type="text"  runat="server" readonly="readonly" style="display: none" />
</div>
</div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

