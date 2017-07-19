<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="CM_Claim_Analysis.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_Analysis" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <script type="text/javascript" language="javascript">
     function showClaimReasonAdd()
     {
        var date=new Date();
        var time=date.getTime();
        var sRet=window.showModalDialog("CM_ClaimReason_Add.aspx?NoUse="+time);
        if(sRet=="refresh")
        {
          window.location.reload();
        }
     }
    
    </script>
    <table width="100%">
    <tr>
    <td>索赔原因统计</td>
    <td align="right">
    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hand" onClick="javascript:showClaimReasonAdd();">
        <asp:Image ID="Image1" Height="16" Width="16" runat="server" ImageUrl="~/Assets/icons/add.gif" />索赔原因</asp:HyperLink>
    </td>
    </tr>
    </table>
    
    
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<div class="RightContent">
     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
              <table width="100%">
              <tr>
              <td style="width:100px" align="left">索赔统计图</td>
              <td align="right" style="width:120px" >请选择统计时间:</td>
              <td align="left" >
              从<asp:TextBox ID="txtKSSJ" runat="server" onClick="setday(this)"></asp:TextBox>
              到<asp:TextBox ID="txtJSSJ" runat="server" onClick="setday(this)"></asp:TextBox>
              &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAnalysis" class="button-outer" runat="server" Text="分 析" 
                      onclick="btnAnalysis_Click" />
                 &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" Text="全 部" class="button-outer" 
                      onclick="btnAll_Click" />     
              </td>
              <td align="right" style="width:30px">
                  <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="返回到 合同索赔界面"  NavigateUrl="~/Contract_Data/CM_Claim_Total.aspx">
             <asp:Image ID="Image3" Height="16" Width="16" runat="server" ImageUrl="~/Assets/icons/back.png" /></asp:HyperLink>
              </td>
              </tr>
              </table>
                   </div>
                   </div>
               </div>
        <div class="box-wrapper">
        <div class="box-outer">
        <div style="text-align:center">
             <asp:CheckBoxList ID="cblSP" runat="server" RepeatColumns="6" RepeatLayout="Table"
                     RepeatDirection="Vertical" AutoPostBack="true" 
                     onselectedindexchanged="cblSP_SelectedIndexChanged">
                </asp:CheckBoxList></div>
            <table width="100%">            
            <tr>
            <td align="center">
			    <asp:chart id="Chart1" runat="server" BackColor="#D3DFF0" Width="700px" 
                    Height="400px" BorderColor="26, 59, 105" Palette="BrightPastel" 
                    BorderDashStyle="Solid" BackSecondaryColor="White" 
                    BackGradientStyle="TopBottom" BorderWidth="2" >
					<titles>
						<asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="索赔问题统计" ForeColor="26, 59, 105"></asp:Title>
					</titles>
					<legends>
						<asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
					</legends>
					<borderskin SkinStyle="Emboss"></borderskin>
					<series>
						<asp:Series IsValueShownAsLabel="True" ChartArea="ChartArea1" Name="Default" CustomProperties="LabelStyle=Bottom" BorderColor="180, 26, 59, 105" LabelFormat=""></asp:Series>
					</series>
					<chartareas>
						<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
							<axisy2 Enabled="False"></axisy2>
							<axisx2 Enabled="False"></axisx2>
							<area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
							<axisy LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
								<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="" />
								<MajorGrid LineColor="64, 64, 64, 64" />
							</axisy>
							<axisx LineColor="64, 64, 64, 64" IsLabelAutoFit="False" ArrowStyle="Triangle">
								<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"  />
								<MajorGrid LineColor="64, 64, 64, 64" />
							</axisx>
						</asp:ChartArea>
					</chartareas>
			   </asp:chart>
            </td>

            </tr>  
            <tr>         
             <td align="center">
             <br />
           <asp:GridView ID="GridView1" width="700PX" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
             <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SPR_DESCRIBLE" HeaderText="原因描述" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="SPR_ACCNUM" HeaderText="累计次数" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
            </td></tr>
         </table>
        </div>
        </div>            
               
</div>
            
</asp:Content>
