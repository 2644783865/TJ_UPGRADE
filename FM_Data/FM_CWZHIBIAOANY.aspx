<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_CWZHIBIAOANY.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_CWZHIBIAOANY" Title="财务指标分析" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   财务指标分析
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
                    &nbsp;&nbsp;&nbsp;
                    年月从:<input type="text" style="width:80px" id="yearmonthstart" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                    到:<input type="text" style="width:80px" id="yearmonthend" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                    <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">查询</a>
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
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="营业收入与营业成本" TabIndex="1">
            <ContentTemplate>
                  <div style="width:100%">
                       <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart1" runat="server" Height="356px" Width="1796px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="营业收入与成本"
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
                                            <asp:Series Name="营业收入">
                                            </asp:Series>
                                            <asp:Series Name="营业成本">
                                            </asp:Series>
                                            <asp:Series Name="营业收入去年同期值">
                                            </asp:Series>
                                            <asp:Series Name="营业成本去年同期值">
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
                                   <asp:Chart ID="Chart6" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="毛利率"
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
                                            <asp:Series Name="毛利率">
                                            </asp:Series>
                                            <asp:Series Name="毛利率去年同期值">
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
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel3" HeaderText="营业成本" TabIndex="2">
            <ContentTemplate>
                  <div style="width:100%">
                     <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart2" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="营业成本"
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
                                            <asp:Series Name="营业成本">
                                            </asp:Series>
                                            <asp:Series Name="营业成本去年同期值">
                                            </asp:Series>
                                            <%--<asp:Series Name="营业成本预算">
                                            </asp:Series>--%>
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
        <asp:TabPanel runat="server" ID="TabPanel4" HeaderText="销售费用" TabIndex="3">
            <ContentTemplate>
                  <div style="width:100%">
                     <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart3" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="销售费用"
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
                                            <asp:Series Name="销售费用">
                                            </asp:Series>
                                            <asp:Series Name="销售费用去年同期值">
                                            </asp:Series>
                                            <%--<asp:Series Name="销售费用预算">
                                            </asp:Series>--%>
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
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel5" HeaderText="管理费用" TabIndex="4">
            <ContentTemplate>
                  <div style="width:100%">
                     <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart4" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="管理费用"
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
                                            <asp:Series Name="管理费用">
                                            </asp:Series>
                                            <asp:Series Name="管理费用去年同期值">
                                            </asp:Series>
                                            <%--<asp:Series Name="管理费用预算">
                                            </asp:Series>--%>
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
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel6" HeaderText="财务费用" TabIndex="5">
            <ContentTemplate>
                  <div style="width:100%">
                     <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart5" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="财务费用"
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
                                            <asp:Series Name="财务费用">
                                            </asp:Series>
                                            <asp:Series Name="财务费用去年同期值">
                                            </asp:Series>
                                            <%--<asp:Series Name="财务费用预算">
                                            </asp:Series>--%>
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
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
        
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="净利润及利润率" TabIndex="7">
            <ContentTemplate>
                  <div style="width:100%">
                     <table width="98%">
                           <tr>
                               <td>
                                   <asp:Chart ID="Chart7" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="净利润"
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
                                            <asp:Series Name="净利润">
                                            </asp:Series>
                                            <asp:Series Name="净利润去年同期值">
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
                                   <asp:Chart ID="Chart8" runat="server" Height="356px" Width="1196px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="利润率"
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
                                            <asp:Series Name="利润率">
                                            </asp:Series>
                                            <asp:Series Name="利润率去年同期值">
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
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
        
    </asp:TabContainer>
    <div style="width:100%" class="box-outer">
          <table width="98%"  class="nowrap cptable fullwidth" align="center">
               <tr>
                   <td align="center" colspan="16">
                       <asp:Label ID="lbtitle1" runat="server" Text="数据明细"></asp:Label>
                   </td>
               </tr>
               <asp:Repeater ID="rptdata0" runat="server">
                  <HeaderTemplate>
                      <tr style="background-color: #B9D3EE;">
                            <td>
                                序号
                            </td>
                            <td>
                                年月 
                            </td>
                            <td>
                                营业成本
                            </td>
                            <%--<td>
                                营业成本预算
                            </td>--%>
                            <td>
                                销售费用
                            </td>
                            <%--<td>
                                销售费用预算
                            </td>--%>
                            <td>
                                管理费用
                            </td>
                            <%--<td>
                                管理费用预算
                            </td>--%>
                            <td>
                                财务费用
                            </td>
                            <%--<td>
                                财务费用预算
                            </td>--%>
                            <td>
                                成本费用合计
                            </td>
                            <%--<td>
                                成本费用预算
                            </td>--%>
                            <td>
                                营业收入
                            </td>
                            <%--<td>
                                营业收入预算
                            </td>--%>
                            <td>
                                利润总额
                            </td>
                            <%--<td>
                                利润总额预算
                            </td>--%>
                      </tr>
                  </HeaderTemplate>
                  <ItemTemplate>
                      <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                            <td>
                                 <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="cw_yearmonth" runat="server" Text='<%#Eval("cw_yearmonth")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="yychengben" runat="server" Text='<%#Eval("yychengben")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="yychengbenys" runat="server" ForeColor="red" Text='<%#Eval("yychengbenys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="xsfeiyong" runat="server" Text='<%#Eval("xsfeiyong")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="xsfeiyongys" runat="server" ForeColor="red" Text='<%#Eval("xsfeiyongys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="glfeiyong" runat="server" Text='<%#Eval("glfeiyong")%>'></asp:Label>
                            </td>
                             <%--<td align="center">
                                <asp:Label ID="glfeiyongys" runat="server" ForeColor="red" Text='<%#Eval("glfeiyongys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="cwfeiyong" runat="server" Text='<%#Eval("cwfeiyong")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="cwfeiyongys" runat="server" ForeColor="red" Text='<%#Eval("cwfeiyongys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="cbfeiyonghj" runat="server" Text='<%#Eval("cbfeiyonghj")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="cbfeiyonghjys" runat="server" ForeColor="red" Text='<%#Eval("cbfeiyonghjys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="yyshouru" runat="server" Text='<%#Eval("yyshouru")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="yyshouruys" runat="server" ForeColor="red" Text='<%#Eval("yyshouruys")%>'></asp:Label>
                            </td>--%>
                            <td align="center">
                                <asp:Label ID="lrzonge" runat="server" Text='<%#Eval("lrzonge")%>'></asp:Label>
                            </td>
                            <%--<td align="center">
                                <asp:Label ID="lrzongeys" runat="server" ForeColor="red" Text='<%#Eval("lrzongeys")%>'></asp:Label>
                            </td>--%>
                      </tr>
                  </ItemTemplate>
               </asp:Repeater>
          </table>
      </div> 
</asp:Content>
