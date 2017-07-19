<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.master"  CodeBehind="CM_InOutRecord.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_InOutRecord" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <p>收支记录</p>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    </asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="false" runat="server">
    </asp:ScriptManager>
    <div class="RightContent">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>

     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr>
                    <td style="width:10px">
                    <asp:Image ID="Image2" style="CURSOR: hand"  ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif" onClick="switchGridVidew(this,'hz')" Height="15" Width="15" runat="server" /></td>
                    <td style="width:100px" align="left">项目合同收支汇总</td>
                       <td align="right" style="width:100px">
                           汇总方式:
                       </td> 
                       <td style="width:25%">
                           <asp:RadioButtonList ID="rblHZFS" runat="server" RepeatColumns="2" 
                               AutoPostBack="true" onselectedindexchanged="rblHZFS_SelectedIndexChanged">
                           <asp:ListItem Text="按项目汇总" Value="0" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="按时间汇总" Value="1"></asp:ListItem>
                           </asp:RadioButtonList>
                       </td>
                       <td>
                           <asp:Panel ID="palXM" runat="server">
                               项目名称：
                               <asp:DropDownList ID="dplXMMC" runat="server" AutoPostBack="true" 
                                   onselectedindexchanged="dplXMMC_SelectedIndexChanged">
                               </asp:DropDownList>
                           </asp:Panel>
                           <asp:Panel ID="palSJ" runat="server" Visible="false">
                               销售合同生效日期：从<asp:TextBox ID="txtSJ1" runat="server" Width="80px" onClick="setday(this);"></asp:TextBox>
                               到
                               <asp:TextBox ID="txtSJ2" runat="server" Width="80px" onClick="setday(this);"></asp:TextBox>
                               &nbsp;
                               <asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="button-outer" onclick="btnQuery_Click" />   
                           </asp:Panel>
                       </td>                      
                    </tr>
                 </table>
             </div>
         </div>
     </div>
    
     <div class="box-wrapper" >
        <div class="box-outer" id="hz" style="display:block;" >   
         <asp:GridView ID="grvTJ" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                onrowcreated="grvTJ_RowCreated1" onrowdatabound="grvTJ_RowDataBound" ShowFooter="true"  AllowPaging="True" PageSize="3" OnDataBound="grvTJ_DataBound">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" BorderStyle="None" BorderWidth="0px" ForeColor="#330099" />
            <PagerSettings Visible="False" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <label><%# Convert.ToInt32(Container.DataItemIndex +1) %></label>
                        <asp:Label ID="lbl_xmbh" runat="server" Text='<%#Eval("XMBH")%>' Visible="false"></asp:Label>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="项目名称" DataField="xmmc" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="项目金额" DataField="xmje" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="到款金额" DataField="xmdkje" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="收款比例" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#string.Format("{0:N2}",(Convert.ToDouble(Eval("xmdkje"))/Convert.ToDouble(Eval("xmje")))*100)+"%" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="合同金额" DataField="htje" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="已付款金额" DataField="yzfje" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="付款比例" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("htje").ToString()=="0"?"0.00%":string.Format("{0:N2}",(Convert.ToDouble(Eval("yzfje"))/Convert.ToDouble(Eval("htje")))*100)+"%" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="索赔金额" DataField="spje" ItemStyle-HorizontalAlign="Right" />
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </asp:GridView>
        <asp:Panel ID="palHZ" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
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
  
     <br />
     <div class="box-inner">
       <div class="box_right">
           <div class="box-title">
             <table>
             <tr>
             <td style="width:15">
                 <asp:Image ID="Image1" style="CURSOR: hand"  ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif" onClick="switchGridVidew(this,'sw')" runat="server" /></td>
                    <td>销售合同信息</td>
             </tr>
             </table>
             </div>
         </div>
     </div>

    
     <div class="box-wrapper">
        <div class="box-outer" id="sw" style="display:block;">   
            <asp:GridView ID="grvSW" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvSW_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Visible="true" ShowFooter="true" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <label><%# Convert.ToInt32(Container.DataItemIndex +1) %></label> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" 
                    ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_BCODE" HeaderText="合同编号" 
                    ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_NAME" HeaderText="合同名称" 
                    ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_JINE" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="PCON_YFK" HeaderText="已付金额" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="支付比例" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%#string.Format("{0:N2}",(Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Eval("PCON_JINE")))*100)+"%" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField HeaderText="索赔金额" DataField="PCON_SPJE" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_STATE").ToString()=="0"?"未开始":Eval("PCON_STATE").ToString()=="1"?"进行中":"完成")+"-"+(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                                     
        </asp:GridView>
        <asp:Panel ID="palSW" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
        </div>
     </div>
   
     <br />
  
          <div class="box-inner">
       <div class="box_right">
           <div class="box-title">
             <table>
             <tr>
             <td style="width:15">
             <asp:Image ID="Image3" style="CURSOR: hand"  ToolTip="隐藏" ImageUrl="~/Assets/images/bar_down.gif" onClick="switchGridVidew(this,'qt')" runat="server" Height="15" Width="15" />
                 </td>
                <td>其他合同信息</td>
             </tr>
             </table>
             </div>
         </div>
     </div>

     <div class="box-wrapper">
        <div class="box-outer" id="qt" style="display:block;">   
            <asp:GridView ID="grvQT" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvQT_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <label><%# Convert.ToInt32(Container.DataItemIndex +1) %></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" 
                    ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_BCODE" HeaderText="合同编号" 
                    ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="PCON_NAME" HeaderText="合同名称" 
                    ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:TemplateField HeaderText="合同类别" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("PCON_FORM").ToString()=="1"?"委外合同":Eval("PCON_FORM").ToString()=="2"?"采购合同":Eval("PCON_FORM").ToString()=="3"?"发运合同":"其他合同" %>'></asp:Label>
               </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PCON_JINE" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="PCON_YFK" HeaderText="已付金额" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="支付比例" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%#string.Format("{0:N2}",(Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Eval("PCON_JINE")))*100)+"%" %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="索赔金额" DataField="PCON_SPJE" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="合同状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_STATE").ToString()=="0"?"未开始":Eval("PCON_STATE").ToString()=="1"?"进行中":"完成")+"-"+(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </asp:GridView>
        <asp:Panel ID="palQT" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
        </div>
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
