<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Packing_Generate.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Packing_Generate" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    装箱单       
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="100%">
        <tr>
            <td style="width:18%">生产制号：
                <asp:Label ID="tsa_id" runat="server" Width="100px"></asp:Label>
                <input id="ms_list" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:18%">项目名称：
                <asp:Label ID="lab_proname" runat="server" Width="100px"></asp:Label>
                <input id="ms_no" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="status" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:18%">工程名称：
                <asp:Label ID="lab_engname" runat="server" Width="100px"></asp:Label>
                <input id="eng_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td align="right">
                <asp:Button ID="packlist" runat="server" Text="装箱单" 
                OnClientClick="return confirm(&quot;确定生成装箱单？&quot;)" onclick="packlist_Click"/>&nbsp;
                <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"/>
            </td>
         </tr>
        </table>
       </div>
     </div>
</div>

 <div class="box-wrapper">
        <asp:Panel ID="Panel1" runat="server" style="height: 87%; width: 99%; overflow: scroll; position: absolute">
        <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" 
                    runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333">
                   <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Eval("MS_ID") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="序号" ItemStyle-Width="30px" 
                            ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Width="30px" 
                                Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"/>
                    <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="MS_NAME" HeaderText="名称" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px"/>
                    <asp:BoundField DataField="MS_UNUM" HeaderText="数量" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"/>
                    <asp:BoundField DataField="MS_UWGHT" HeaderText="单重" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"/>
                    <asp:BoundField DataField="MS_TLWGHT" HeaderText="总重" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"/>
                    <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px"/>
                    <asp:TemplateField HeaderText="箱号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="xianghao" runat="server" style="border-style:none; width:100%" 
                                    type="text" value='<%#Eval("MS_TIMERQ") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    <%--<asp:BoundField DataField="MS_TIMERQ" HeaderText="箱号" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"/>--%>
                    <asp:BoundField DataField="MS_NOTE" HeaderText="备注" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="MS_DELIVERY" HeaderText="是否发运" Visible="false" 
                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"/>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
        </asp:Panel>
        </div>
</asp:Content>

