<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="CM_Claim_Summary.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_Summary" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">索赔信息汇总(扣款后)
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
        <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                  <td style="width:10px">
                    <asp:Image ID="Image2" style="CURSOR: hand"  ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif" onClick="switchGridVidew(this,'hz')" Height="15" Width="15" runat="server" /></td>
                    <td align="left" style="width:10%">索赔汇总信息</td>
                    <td align="right" style="width:20%">请选择汇总类别</td>
                    <td align="left">
                        <asp:RadioButtonList ID="rblHZLB" runat="server" RepeatColumns="2" 
                            RepeatDirection="Horizontal"  AutoPostBack="true"
                            onselectedindexchanged="rblHZLB_SelectedIndexChanged">
                        <asp:ListItem Text="项目" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="时间" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="center">
                        <asp:Panel ID="palXMHZ" runat="server">
                            <asp:DropDownList ID="dplXMMC" runat="server" AutoPostBack="true" onselectedindexchanged="dplXMMC_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="palSJHZ" runat="server" Visible="false">
                        从<asp:TextBox ID="txtKJSJ" runat="server" onClick="setday(this)"></asp:TextBox>
                        到<asp:TextBox ID="txtJSSJ" runat="server"  onClick="setday(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnQuery" runat="server" Text="查 询" />
                        </asp:Panel>
                    </td>
                    <td align="right">
                        <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="返回到 合同索赔界面" NavigateUrl="~/Contract_Data/CM_Claim_Total.aspx">
                        <asp:Image ID="Image3" Height="16" Width="16" runat="server" ImageUrl="~/Assets/icons/back.png" /></asp:HyperLink>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    
    <div class="box-wrapper">
     <div class="box-outer" style="display:block" id="hz">
         <asp:GridView ID="grvHZ" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
             ShowFooter="true" onrowdatabound="grvHZ_RowDataBound" AllowPaging="True" PageSize="10">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" BorderStyle="None" BorderWidth="0px" ForeColor="#330099" />
            <PagerSettings Visible="False" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="XMMC" HeaderText="项目名称" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="SPDH" HeaderText="索赔单号" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="YZ" HeaderText="业主索赔" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="ZJYZ" HeaderText="重机向业主索赔" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
               <asp:BoundField DataField="ZJFBS" HeaderText="重机向分包商/供应商索赔" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="FBS" HeaderText="分包商/供应商索赔" 
                    ItemStyle-HorizontalAlign="Center" >

                </asp:BoundField>
                <asp:TemplateField HeaderText="索赔差额" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"  Text='<%# Convert.ToDecimal(Eval("ZJFBS").ToString())+Convert.ToDecimal(Eval("ZJYZ").ToString())-Convert.ToDecimal(Eval("YZ").ToString())-Convert.ToDecimal(Eval("FBS").ToString()) %>'
                    ForeColor='<%# (Convert.ToDecimal(Eval("ZJFBS").ToString())+Convert.ToDecimal(Eval("ZJYZ").ToString())-Convert.ToDecimal(Eval("YZ").ToString())-Convert.ToDecimal(Eval("FBS").ToString()))<0?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="White" ForeColor="Blue" HorizontalAlign="Center" />                   
        </asp:GridView>
        <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
         <asp:Panel ID="Pal_page" runat="server">            
        <div style="text-align:center">
        第<asp:Label ID="lbl_currentpage" runat="server" Text=""></asp:Label>/<asp:Label ID="lbl_totalpage" runat="server" Text=""></asp:Label>
        <asp:LinkButton ID="lnkbtnFrist" runat="server" OnClick="lnkbtnFrist_Click">首页</asp:LinkButton> 
        <asp:LinkButton ID="lnkbtnPre" runat="server" OnClick="lnkbtnPre_Click">上一页</asp:LinkButton>
        <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click">下一页</asp:LinkButton> 
        <asp:LinkButton ID="lnkbtnLast" runat="server" OnClick="lnkbtnLast_Click">尾页</asp:LinkButton> 
        跳转到第<asp:TextBox ID="txt_goto" runat="server" Width="50px"></asp:TextBox>页
        <asp:LinkButton ID="lnkbtnGoto" runat="server" OnClick="lnkbtnGoto_Click">GO</asp:LinkButton> 
            <asp:RegularExpressionValidator ControlToValidate="txt_goto" ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确的数据格式！" ValidationExpression="^[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
       </div>
       </asp:Panel>
     </div>
     </div>
            
</asp:Content>
