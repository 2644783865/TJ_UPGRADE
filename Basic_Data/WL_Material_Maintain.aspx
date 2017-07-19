<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="WL_Material_Maintain.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.WL_Material_Maintain" Title="添加物料信息" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
      
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td>
                物料大类：<asp:DropDownList ID="DDLclass" runat="server" AutoPostBack ="true" 
                    onselectedindexchanged="DDLclass_SelectedIndexChanged">
            </asp:DropDownList>
                物料种类：<asp:DropDownList ID="DDLname" runat="server">
            </asp:DropDownList>
            </td>
            <td align="right">
                <asp:RangeValidator ID="TextBoxappendRangeValidator" runat="server" 
                    ErrorMessage="追加行数范围为1到20" ControlToValidate="TextBoxappend" MinimumValue="1" 
                    MaximumValue="20" Type="Integer"></asp:RangeValidator>
                追加行数：<asp:TextBox ID="TextBoxappend" runat="server" ForeColor="Blue" Width="70px"></asp:TextBox>
                <asp:Button ID="append" runat="server" Text="追加行" onclick="append_Click" CausesValidation="false" />&nbsp;&nbsp;
                <asp:Button ID="delete" runat="server" Text="删除" CausesValidation="false" onclick="delete_Click" />&nbsp;&nbsp;
                <asp:Button ID="confirm" runat="server" Text="提交" onclick="confirm_Click" />&nbsp;&nbsp;
                <asp:Button ID="goback" runat="server" Text="返回"  CausesValidation="false" onclick="goback_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    
    <asp:Label ID="message" runat="server" ForeColor="Red"></asp:Label>   
    
    <div class="box-wrapper">
    <div class="box-outer">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="100%"
            EmptyDataText="没有相关数据！">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" BorderStyle="None" BackColor="#EFF3FB" />
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.DataItemIndex+1%>' BorderStyle="None" BackColor="#EFF3FB"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="名称" >
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxname" runat="server"   Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" runat="server" ErrorMessage="" ControlToValidate="TextBoxname" Text="*"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="规格">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxstandard" runat="server"  Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="standardRequiredFieldValidator" runat="server" ErrorMessage="规格不能为空" ControlToValidate="TextBoxstandard" Text="*"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="材质">
                    <ItemTemplate>
                        <asp:TextBox ID="Textcaizhi" runat="server" Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>                      
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="国标">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxguobiao" runat="server" Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>                      
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="助记码">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxhelpcode" runat="server"   Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="米重">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxmeterweight" runat="server"  Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="meterweightRequiredFieldValidator" runat="server" ErrorMessage="" ControlToValidate="TextBoxmeterweight" Text="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="meterweightRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxmeterweight"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="米面积">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxmeterarea" runat="server"  Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="meterareaRequiredFieldValidator" runat="server" ErrorMessage="" ControlToValidate="TextBoxmeterarea" Text="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="meterareaRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxmeterarea"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="单位" >
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxunit" runat="server"   Width="50px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="unitRequiredFieldValidator" runat="server" ErrorMessage="单位不能为空" ControlToValidate="TextBoxunit" Text="*"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="价格">
                    <ItemTemplate >
                        <asp:TextBox ID="TextBoxprice" runat="server"   Width="100px" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="priceRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" ControlToValidate="TextBoxprice" Text="*"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <asp:RadioButtonList ID="RadioButtonListstatus" runat="server" RepeatColumns="2" AutoPostBack="False" DataValueField='<%# DataBinder.Eval(Container.DataItem, "status")%>' BorderStyle="None" BackColor="#EFF3FB">
                            <asp:ListItem Value="0"  Text="停用" ></asp:ListItem>
                            <asp:ListItem Value="1" Selected="True" Text="在用" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxcomment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comment")%>' BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>

            </Columns>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="#EFF3FB" />
    </asp:GridView>
    </table>
     
     </div><!--box-outer END -->
     </div> <!--box-wrapper END -->                 
     
</asp:Content>
