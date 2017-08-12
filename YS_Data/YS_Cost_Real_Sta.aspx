<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="YS_Cost_Real_Sta.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Real_Sta" Title="预算分析统计" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    预算分析统计
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="FixTable.css" rel="stylesheet" type="text/css" />
    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server"  Visible="false"></asp:Label>
        <div style="width:100%">
        <table width="98%">
             <tr>
                <td>
                    &nbsp;&nbsp;&nbsp; 年月从:<input type="text" style="width:80px" id="yearmonthstart" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}" runat="server" class="easyui-datebox" />&nbsp;&nbsp; 
                    到:<input type="text" style="width:80px" id="yearmonthend" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                    <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">
                    查询</a>
                </td>
                <td align="right">
                   <asp:Label runat="server" ID="notice0" Text="提示：鼠标停留在条形上可看到数值" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" 
         ActiveTabIndex="0" >       
         <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="预算获利统计" TabIndex="4">
            <ContentTemplate>
                  <div style="width:100%">
                       <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart1" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="预算利润统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="预算毛利润">
                                            </asp:Series>
                                            <asp:Series Name="实际毛利润">
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
                               </td>
                           </tr>
                           
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart2" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="预算利率统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：%" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="毛利率预算值">
                                            </asp:Series>
                                            <asp:Series Name="毛利率实际值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea2" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>                              
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>    
        
        
         <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="预算材料费用统计" TabIndex="3">
            <ContentTemplate>
                  <div style="width:100%">
                       <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart3" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="预算材料费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="材料费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="材料费用实际总值">
                                            </asp:Series>
                                            
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea3" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart4" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="黑色金属费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="黑色金属费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="黑色金属费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea4" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart5" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="外购件费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="外购件费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="外购件费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea5" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart6" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="加工件费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="加工件费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="加工件费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea6" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart7" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="油漆涂料费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="油漆涂料费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="油漆涂料费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea7" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart8" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="电气电料费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="电气电料费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="电气电料费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea8" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart9" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="其他材料费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="其他材料费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="其他材料费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea9" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
        
        
         <asp:TabPanel runat="server" ID="TabPanel3" HeaderText="预算人工费用统计" TabIndex="2">
            <ContentTemplate>
                  <div style="width:100%">
                       <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart10" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="预算人工费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="人工费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="人工费用实际总值">
                                            </asp:Series>
                                            
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea10" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart11" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="直接人工费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="直接人工费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="直接人工费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea11" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart12" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="厂内分包费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="厂内分包费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="厂内分包费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea12" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart13" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="生产外协费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="生产外协费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="生产外协费用实际总值">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea13" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           

                                                    
                           
                        
                                                 
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>   
        
        
         <asp:TabPanel runat="server" ID="TabPanel4" HeaderText="预算发运费用统计" TabIndex="1">
            <ContentTemplate>
                  <div style="width:100%">
                       <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart14" runat="server" Height="356px" Width="1000px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="预算发运费用统计"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        
                                        
                                        <Legends>
                                            <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                <CustomItems>
                                                    <asp:LegendItem Name="单位：万元" BorderColor="White">
                                                    </asp:LegendItem>
                                                </CustomItems>
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="发运费用预算总值">
                                            </asp:Series>
                                            <asp:Series Name="发运费用实际总值">
                                            </asp:Series>
                                            
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea14" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                               </td>
                           </tr>
                           
                          
                           

                                                    
                           
                        
                                                 
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>         
    </asp:TabContainer>
</asp:Content>
