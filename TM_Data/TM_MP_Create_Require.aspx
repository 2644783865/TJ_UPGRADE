<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_Create_Require.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Create_Require" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划表     
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />
 <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
 <script src="../JS/SQL.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<%--<script type="text/javascript">
    function SelTechPersons(ctl00_PrimaryContent_GridView1,obj)
    {
        var i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
        var objId=obj.id;
        document.getElementById(objId).value=i;
    }
</script>--%>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="box-inner">
   <div class="box_right">
    <div class="box-title">
        <table width="99%">
        <tr>
            <td style="width:20%">生产制号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
                <input id="tablename" type="text" runat="server"  readonly="readonly" value="" style="display:none"/>
            </td>
            <td style="width:20%">项目名称：
                <asp:Label ID="lab_proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:20%">工程名称：
                <asp:Label ID="lab_engname" runat="server"></asp:Label>
                <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td align="right">
                 增加行数:<input ID="txtnum" type="text" value="" runat="server" style="width:50px"/>&nbsp;&nbsp;
                <asp:Button ID="btnadd" runat="server" Text="新 增"  OnClientClick="return chkadd()" onclick="btnadd_Click"/>
                &nbsp;&nbsp;
                <asp:Button ID="btninsert" runat="server" Text=" 插 入" OnClientClick="return insert()" onclick="btninsert_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Text="删 除"  OnClientClick="return check()" onclick="btndelete_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnsave" runat="server" Text="保 存" onclick="btnsave_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           </td>   
         </tr>
        </table>
       </div>
     </div>
</div>

    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="Panel1" runat="server" style="height:560px; width: 100%; overflow: scroll; position:relative">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" 
                    runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                   <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                        <ItemTemplate>
                            <asp:Label ID="Index" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料编码">
                        <ItemTemplate>
                            <asp:TextBox ID="marid" runat="server" BorderStyle="None" onblur="outAutoCode(this)" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "MP_MARID")%>' Width="120px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" 
                                    CompletionSetCount="10" DelimiterCharacters="" Enabled="True"
                                    MinimumPrefixLength="1" ServiceMethod="HmCode" ServicePath="~/Ajax.asmx" 
                                    TargetControlID="marid" UseContextKey="True">
                                </cc1:AutoCompleteExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料名称">
                        <ItemTemplate>
                            <input ID="name" runat="server" value='<%# Eval("MP_NAME") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格">
                        <ItemTemplate>
                            <input ID="guige" runat="server" value='<%# Eval("MP_GUIGE") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质">
                        <ItemTemplate>
                            <input ID="caizhi" runat="server" value='<%# Eval("MP_CAIZHI") %>' style="border-style:none; width:120px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <ItemTemplate>
                            <input ID="unit" runat="server" value='<%# Eval("MP_UNIT") %>' style="border-style:none; width:60px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="重量">
                        <ItemTemplate>
                            <input ID="weight" runat="server" value='<%# Eval("MP_WEIGHT") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <input ID="number" runat="server" value='<%# Eval("MP_NUMBER") %>' style="border-style:none; width:60px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标">
                        <ItemTemplate>
                            <input ID="standard" runat="server" value='<%# Eval("MP_STANDARD") %>' style="border-style:none; width:120px" type="text" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用途" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <input ID="application" runat="server" value='<%# Eval("MP_USAGE") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="时间要求" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <input ID="time" runat="server" value='<%# Eval("MP_TIMERQ") %>' onclick="setday(this)" style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="材料类别" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <input ID="category" runat="server" value='<%# Eval("MP_TYPE") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="环保影响" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <input ID="influence" runat="server" value='<%# Eval("MP_ENVREFFCT") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <input ID="remark" runat="server" value='<%# Eval("MP_NOTE") %>' style="border-style:none; width:80px" type="text" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
    
    <div>
            <input id="txtid" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="add_id" type="text" runat="server" readonly="readonly" style="display: none" />
            <input id="istid" type="text" runat="server" readonly="readonly" style="display: none" />
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

