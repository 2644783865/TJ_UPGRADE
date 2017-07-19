<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="CM_InOut_Analysis.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_InOut_Analysis" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <p>收支记录</p>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    </asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
    <div class="RightContent">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
   <div class="RightContent">
    <div class="box-wrapper">
    <div style="height:8px" class="box_top"></div>
        <div class="box-outer">        
        <table width="100%" >
                    <tr>                    
                    <td style="width:200px;" align="left"><strong>项目合同收支汇总：</strong></td>
                       
                       <td>                           
                          项目名称：
                      <asp:TextBox ID="tb_pjinfo" runat="server"  Text=""  OnTextChanged="tb_pjinfo_Textchanged" AutoPostBack="True"></asp:TextBox>
                
             <asp:LinkButton ID="lkb_confirm" runat="server" OnClick="lkb_confirm_Click" PostBackUrl="">
                 <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icon-fuction/139.gif" AlternateText="确定" /></asp:LinkButton>
                                            
             <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_pjinfo"
                   ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                   ServiceMethod="GetPJNAME" FirstRowSelected="true"  CompletionListCssClass="autocomplete_completionListElement"
                 CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" >
                </asp:AutoCompleteExtender>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     项目编号：<asp:TextBox ID="tb_pjid" runat="server" Enabled="false"></asp:TextBox></td>                     
                    </tr>
                 </table>
  
      <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" 
                            TabStripPlacement="Top" ActiveTabIndex="0">
         <asp:TabPanel ID="TabTJ" runat="server" HeaderText="收支分析" TabIndex="0">
          <ContentTemplate>                   
        
         <asp:GridView ID="grvTJ" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                onrowcreated="grvTJ_RowCreated" onrowdatabound="grvTJ_RowDataBound" ShowFooter="true" 
                 EmptyDataText="没有记录">            
            <RowStyle BackColor="#EFF3FB" />            
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
                    <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToDouble(Eval("xmje"))==0?"0.00%":string.Format("{0:N2}",(Convert.ToDouble(Eval("xmdkje"))/Convert.ToDouble(Eval("xmje")))*100)+"%" %>'></asp:Label>
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
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />           
            <AlternatingRowStyle BackColor="White" />                             
        </asp:GridView>
        
         <asp:Panel ID="Pal_page" runat="server" Visible="false">            
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
         <br />
         <div>
       <table  width="100%">
       <tr >
       <td style="text-align:center;">
        <asp:Chart ID="Chart1" runat="server" Height="296px" Width="768px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
            Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White"
            BackGradientStyle="TopBottom" BorderWidth="2" BackColor="#D3DFF0" BorderColor="26, 59, 105">
            <Titles>
                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="项目收入支出对比"
                    ForeColor="26, 59, 105">
                </asp:Title>
            </Titles>
            <Legends>
                <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                    <%--<CustomItems>
                        <asp:LegendItem Name="支出" Color="252, 180, 65" MarkerStyle="Circle" ImageStyle="Marker"
                            MarkerSize="10">
                        </asp:LegendItem>
                        <asp:LegendItem Name="收入" Color="85,102,255" MarkerStyle="Triangle" ImageStyle="Marker"
                            MarkerSize="10">
                        </asp:LegendItem>
                    </CustomItems>--%>
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="Emboss"></BorderSkin>
            <Series>
                <asp:Series Name="收支对比" BorderColor="180, 26, 59, 105" ChartType="Column">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                    BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                    BackGradientStyle="TopBottom">
                    <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                        WallWidth="0" IsClustered="False"></Area3DStyle>
                    <AxisY LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64" ArrowStyle="SharpTriangle">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
     </td></tr></table>
       </div>
       </ContentTemplate>
       </asp:TabPanel>
       
       <asp:TabPanel ID="TabSW" runat="server" HeaderText="销售合同" TabIndex="0">
          <ContentTemplate> 
          <asp:GridView ID="grvSW" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvSW_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Visible="true" ShowFooter="true" EmptyDataText="没有记录">
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
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
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
        
       </ContentTemplate>
       </asp:TabPanel>
       
       <asp:TabPanel ID="TabFB" runat="server" HeaderText="委外合同" TabIndex="1">
          <ContentTemplate> 
          <asp:GridView ID="grvFB" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvFB_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" EmptyDataText="没有记录">
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
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
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
       </ContentTemplate>
       </asp:TabPanel>
       
       <asp:TabPanel ID="TabCG" runat="server" HeaderText="采购合同" TabIndex="2">
          <ContentTemplate> 
          <asp:GridView ID="grvCG" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvCG_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" EmptyDataText="没有记录">
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
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
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
       </ContentTemplate>
       </asp:TabPanel>
       
       <asp:TabPanel ID="TabFY" runat="server" HeaderText="运输合同" TabIndex="3">
          <ContentTemplate> 
          <asp:GridView ID="grvFY" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvFY_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" EmptyDataText="没有记录">
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
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
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
       </ContentTemplate>
       </asp:TabPanel>
       
       <asp:TabPanel ID="TabQT" runat="server" HeaderText="其他合同" TabIndex="4">
          <ContentTemplate> 
           <asp:GridView ID="grvQT" width="100%" CssClass="toptable grid" runat="server" onrowdatabound="grvQT_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" EmptyDataText="没有记录">
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
                    <asp:Label ID="Label1" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>' ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
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
        
       </ContentTemplate>
       </asp:TabPanel>
       
        </asp:TabContainer>
    
     </div>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
