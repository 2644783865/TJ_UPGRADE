<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_HTZHIBIAOANY.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_HTZHIBIAOANY" Title="合同指标分析" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   合同指标分析
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
                    日期从:<input type="text" style="width:100px" id="datestart" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                    到:<input type="text" style="width:100px" id="dateend" runat="server" class="easyui-datebox" />&nbsp;&nbsp;
                    <a id="btnsearch" class="easyui-linkbutton" runat="server" onserverclick="btnsearch_click">查询</a>
                </td>
           </tr>
        </table>
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" 
         ActiveTabIndex="0" >
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="收入与合同情况" TabIndex="0">
            <ContentTemplate>
                  <div style="width:100%">
                      <table width="98%">
                           <tr>
                               <td colspan="2">
                                   <asp:Chart ID="Chart0" runat="server" Height="360px" Width="1096px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="收入与合同情况"
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
                                            <asp:Series Name="新签合同额">
                                            </asp:Series>
                                            <%--<asp:Series Name="预计新签合同额">
                                            </asp:Series>--%>
                                            <asp:Series Name="结转合同额">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea0" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
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
                                 <asp:Chart ID="Chart1" runat="server" Height="320px" Width="568px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                    <Titles>
                                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="新签合同组成"
                                            ForeColor="26, 59, 105">
                                        </asp:Title>
                                    </Titles>
                                    
                                    
                                    <Legends>
                                        <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                            <CustomItems>
                                            </CustomItems>
                                        </asp:Legend>
                                    </Legends>
                                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                    <Series>
                                        <asp:Series Name="新签合同组成">
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
                             <td>
                                 <asp:Chart ID="Chart2" runat="server" Height="320px" Width="568px" Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="VerticalCenter" BorderWidth="2px" BackColor="211, 223, 240" BorderColor="#1A3B69">
                                    <Titles>
                                        <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="结转合同组成"
                                            ForeColor="26, 59, 105">
                                        </asp:Title>
                                    </Titles>
                                    
                                    
                                    <Legends>
                                        <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                            <CustomItems>
                                            </CustomItems>
                                        </asp:Legend>
                                    </Legends>
                                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                    <Series>
                                        <asp:Series Name="结转合同组成">
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
                  <div style="width:96%" class="box-outer">
                      <table width="92%"  class="nowrap cptable fullwidth" align="center">
                           <tr>
                               <td align="center" colspan="12">
                                   <asp:Label ID="lbtitle1" runat="server" Text="数据明细"></asp:Label>
                               </td>
                           </tr>
                           <asp:Repeater ID="rptdata0" runat="server">
                              <HeaderTemplate>
                                  <tr style="background-color: #B9D3EE;">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center" width="20px">
                                            装备集团外(已审核)
                                        </td>
                                        <td align="center">
                                            装备集团外(未审核)
                                        </td>
                                        <td align="center">
                                            装备集团内(已审核)
                                        </td>
                                        <td align="center">
                                            装备集团内(未审核)
                                        </td>
                                        <td align="center">
                                            自营(已审核)
                                        </td>
                                        <td align="center">
                                            自营(未审核)
                                        </td>
                                        <td align="center">
                                            新签(已审核)
                                        </td>
                                        <td align="center">
                                            新签(未审核)
                                        </td>
                                        <%--<td align="center">
                                            预计新签
                                        </td>--%>
                                        <td align="center">
                                            结转(已审核)
                                        </td>
                                        <td align="center">
                                            结转(未审核)
                                        </td>
                                  </tr>
                              </HeaderTemplate>
                              <ItemTemplate>
                                  <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                        <td align="center">
                                             <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhte_jtwsh" runat="server" Text='<%#Eval("newhte_jtwsh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhte_jtwwsh" runat="server" Text='<%#Eval("newhte_jtwwsh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhte_jtnsh" runat="server" Text='<%#Eval("newhte_jtnsh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhte_jtnwsh" runat="server" Text='<%#Eval("newhte_jtnwsh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhte_zysh" runat="server" Text='<%#Eval("newhte_zysh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhte_zywsh" runat="server" Text='<%#Eval("newhte_zywsh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhtesh" runat="server" Text='<%#Eval("newhtesh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="newhtewsh" runat="server" Text='<%#Eval("newhtewsh")%>'></asp:Label>
                                        </td>
                                        <%--<td align="center">
                                            <asp:Label ID="ht_yusuanhte" runat="server" Text='<%#Eval("ht_yusuanhte")%>'></asp:Label>
                                        </td>--%>
                                        <td align="center">
                                            <asp:Label ID="jzhtesh" runat="server" Text='<%#Eval("jzhtesh")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="jzhtewsh" runat="server" Text='<%#Eval("jzhtewsh")%>'></asp:Label>
                                        </td>
                                  </tr>
                              </ItemTemplate>
                           </asp:Repeater>
                      </table>
                  </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer> 
</asp:Content>
