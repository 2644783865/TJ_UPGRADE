<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PD_DocpinshenInfo.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.PD_DocpinshenInfo" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    合同评审 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_confirm" runat="server" Text="提交" 
                                    onclick="btn_confirm_Click"  />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" onclick="btn_back_Click"  />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Lbstatus" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbyesorno" runat="server" Text="Y" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>               
                   <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                        Width="100%">
                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="合同信息" TabIndex="0">
                            <HeaderTemplate>
                                投标信息
                            </HeaderTemplate>
                            <ContentTemplate>
                               <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <tr>
                                        <td width="30%" height="25" align="right">
                                            投标基本信息：</td>
                                        <td width="70%" class="category">
                                            <table style="width: 100%"  cellpadding="4" class="toptable grid"
                                                cellspacing="1" border="1" >
                                                <tr>
                                                    <td style="width: 15%">
                                                        项目名称：
                                                    </td>
                                                    <td style="width: 85%">
                                                        <asp:Label ID="lb_pronm" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 15%">
                                                        设备名称：
                                                    </td>
                                                    <td style="width: 85%">
                                                        <asp:Label ID="lb_engnm" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        业主：</td>
                                                    <td>
                                                        <asp:Label ID="lb_yz" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        金额：</td>
                                                    <td>
                                                        <asp:Label ID="lb_je" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        商务负责人：</td>
                                                    <td>
                                                        <asp:Label ID="lb_swfzr" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        备注：</td> 
                                                        <td>
                                                            <asp:Label ID="lbMeno" runat="server" Text="Label"></asp:Label>
                                                        </td>                               
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="right">
                                            投标信息：</td>
                                        <td class="category" >
                                            <table width="100%"  class="toptable grid" >
                                                 <tr>
                                                    <td>
                                                    <asp:Label ID="filesError" runat="server" Text="Label" Visible="False" 
                                                            ForeColor="Red"></asp:Label>
                                                       <asp:GridView ID="ViewGridViewFiles" runat="server"  
                                                            AutoGenerateColumns="False" PageSize="5"
                                                            DataKeyNames="fileID" Width="50%" ForeColor="#333333" >
                                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <Columns>
                                                            <asp:BoundField DataField="fileName" HeaderText="文件名称">
                                                                <ControlStyle Font-Size="Small" />
                                                                <ItemStyle  HorizontalAlign="Center"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="fileUpDate" HeaderText="文件上传时间">
                                                                <ControlStyle Font-Size="Small" />
                                                                <ItemStyle  HorizontalAlign="Center"/>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="文件下载">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDF" runat="server"  ImageUrl="~/Assets/images/文件下载.gif" OnClick="imgbtnDF_Click" oninit="imgbtnDF_Init" Height="15px" Width="15px" CausesValidation="False"/>   
                                                            </ItemTemplate>
                                                                <ControlStyle Font-Size="Small" />
                                                                <ItemStyle  HorizontalAlign="Center"/>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                     <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" Font-Size="X-Small" ForeColor="White" Height="10px" />
                                                     <RowStyle BackColor="#EFF3FB" />
                                                    </asp:GridView>
                                                        
                                                   </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>      
                                 </table>                              
                            </ContentTemplate>
                        </asp:TabPanel>
                        

                        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="评审信息" TabIndex="1">
                            <ContentTemplate>
                             <div style="border: 1px solid #000000;">
                                 <asp:Panel ID="Pan_shenheren" runat="server">
                                    <table width="100%" cellpadding="4" border="1" cellspacing="1"  class="toptable grid">
                                        <asp:Panel ID="Panel1" runat="server" Enabled="false" Visible="False">
                                           <tr>
                                                <td align="left" colspan="2" >
                                                    审核部门:<asp:TextBox ID="PD_shenhebumen1" runat="server" Text="技术部" ReadOnly="true" CssClass="text1style"></asp:TextBox>
                                                   
                                                    审核人:<asp:TextBox ID="PD_shenheren1" runat="server" 
                                                       ReadOnly="true" CssClass="text1style">
                                                        </asp:TextBox>
                                                    审核时间:<asp:TextBox ID="PD_shenhedate1" runat="server" 
                                                     CssClass="text1style" ReadOnly="true" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%" align="center">
                                                    审核意见:</td>
                                                    <td >
                                                   <asp:TextBox ID="PD_shenheyj1" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                           <tr>
                                           <td align="center">评审结论:</td>
                                              <td>
                                               <asp:RadioButtonList ID="PD_jielun1" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioJSB_CheckedChanged" >
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </asp:Panel>  
                                       <asp:Panel ID="Panel2" Visible="false" Enabled="false" runat="server" >
                                           <tr>
                                                <td align="left" colspan="2" style="border-top: solid 2px black";>
                                                
                                                    审核部门:<asp:TextBox ID="PD_shenhebumen2" runat="server" Text="采购部" ReadOnly="true" CssClass="text1style"></asp:TextBox>
                                                    
                                                    审核人:<asp:TextBox ID="PD_shenheren2" runat="server" ReadOnly="true">
                                                        </asp:TextBox>
                                                    审核时间:<asp:TextBox ID="PD_shenhedate2" runat="server" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    审核意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj2" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                           <td align="center">评审结论:</td>
                                              <td>
                                               <asp:RadioButtonList ID="PD_jielun2" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioCGB_CheckedChanged" >
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel>
                                       <asp:Panel ID="Panel3" Enabled="false" Visible="false" runat="server" >
                                            <tr>
                                                <td align="left" colspan="2" style="border-top: solid 2px black";>
                                                
                                                    审核部门:<asp:TextBox ID="PD_shenhebumen3" runat="server" Text="生产部" ReadOnly="true"  CssClass="text1style"></asp:TextBox>
                                                    审核人:<asp:TextBox ID="PD_shenheren3" runat="server" 
                                                         ReadOnly="true">
                                                        </asp:TextBox>
                                                    审核时间:<asp:TextBox ID="PD_shenhedate3" runat="server"
                                                      ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    审核意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj3" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="center">评审结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun3" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioSCB_CheckedChanged" >
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel> 
                                       <asp:Panel ID="Panel4" Enabled="false" Visible="false" runat="server" >
                                            <tr>
                                                <td align="left"  colspan="2" style="border-top: solid 2px black";>
                                                    审核部门:<asp:TextBox ID="PD_shenhebumen4" runat="server" Text="质量部" ReadOnly="true"  CssClass="text1style"></asp:TextBox>
                                                    审核人:<asp:TextBox ID="PD_shenheren4" runat="server" ReadOnly="true"
                                                       >
                                                        </asp:TextBox>
                                                    审核时间:<asp:TextBox ID="PD_shenhedate4" runat="server" 
                                                     ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  style="width: 10%" align="center">
                                                    审核意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj4" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="center">评审结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun4" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioZLB_CheckedChanged">
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                           
                                       </asp:Panel> 
                                        <asp:Panel ID="Panel5" Enabled="false" Visible="false" runat="server" >
                                            <tr>
                                                <td align="left" style="border-top: solid 2px black"; colspan="2">
                                                    审核部门:<asp:TextBox ID="PD_shenhebumen5" Text="质量部" ReadOnly="true" runat="server"  CssClass="text1style"></asp:TextBox>
                                                    审核人:<asp:TextBox ID="PD_shenheren5" runat="server" >
                                                        </asp:TextBox>
                                                    审核时间:<asp:TextBox ID="PD_shenhedate5" runat="server" 
                                                    ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  align="center">
                                                    审核意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj5" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="center">评审结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun5" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioCYB_CheckedChanged">
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel>
                                       
                                       <asp:Panel ID="Panel6" Enabled="false" Visible="false" runat="server" >
                                           <tr>
                                                <td align="left" colspan="2" style="border-top: solid 2px black";>
                                                    审核部门:<asp:TextBox ID="PD_shenhebumen6" runat="server" Text="审  计" ReadOnly="true" ></asp:TextBox>
                                                    审核人:<asp:TextBox ID="PD_shenheren6" runat="server" ></asp:TextBox>
                                                    审核时间:<asp:TextBox ID="PD_shenhedate6" runat="server" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    审核意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj6" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                           <tr>
                                               <td align="center">审核结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun6" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioSJ_CheckedChanged" >
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel> 
                                                                                                    
                                    </table>
                                 </asp:Panel>
                               </div>
                            </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="TabPanel3" HeaderText="批准信息" TabIndex="2">
                            <ContentTemplate>
                                 <table width="100%" cellpadding="4" border="1" cellspacing="1" class="toptable grid">
                                       <asp:Panel ID="Panel7" Enabled="false" Visible="false" runat="server" >
                                            <tr>
                                                <td align="left"  colspan="2" style="border-top: solid 2px black";>
                                                    批准人:<asp:TextBox ID="PD_shenheren7" runat="server" 
                                                       >
                                                        </asp:TextBox>
                                                    批准时间:<asp:TextBox ID="PD_shenhedate7" runat="server" 
                                                     ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    批准意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj7" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="center">批准结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun7" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioPZR1_CheckedChanged">
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel>
                                       <asp:Panel ID="Panel8" Enabled="false" Visible="false" runat="server" >
                                            <tr>
                                                <td align="left"  colspan="2" style="border-top: solid 2px black";>
                                                   
                                                    批准人:<asp:TextBox ID="PD_shenheren8" runat="server" 
                                                      >
                                                        </asp:TextBox>
                                                    批准时间:<asp:TextBox ID="PD_shenhedate8" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    批准意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj8" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                               <td align="center">批准结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun8" RepeatColumns="2" Height="20px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioPZR2_CheckedChanged" >
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel>
                                       
                                        <asp:Panel ID="Panel9" Enabled="false" Visible="false" runat="server" >
                                             <tr>
                                                <td align="left"  colspan="2" style="border-top: solid 2px black";>
                                                    批准人:<asp:TextBox ID="PD_shenheren9" runat="server" >
                                                        </asp:TextBox>
                                                    批准时间:<asp:TextBox ID="PD_shenhedate9" runat="server" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    批准意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj9" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                           <tr>
                                               <td align="center">批准结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun9" RepeatColumns="2" Height="20px" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="RadioPZR3_CheckedChanged">
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel> 
                                       
                                       <asp:Panel ID="Panel10" Enabled="false" Visible="false" runat="server" >
                                             <tr>
                                                <td align="left"  colspan="2" style="border-top: solid 2px black";>
                                                    批准人:<asp:TextBox ID="PD_shenheren10" runat="server" 
                                                       >
                                                        </asp:TextBox>
                                                    批准时间:<asp:TextBox ID="PD_shenhedate10" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    批准意见:
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="PD_shenheyj10" Columns="100" Rows="5" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                           <tr>
                                               <td align="center">批准结论:</td>
                                                <td>
                                                    <asp:RadioButtonList ID="PD_jielun10" RepeatColumns="2" Height="20px" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="RadioPZR4_CheckedChanged" >
                                                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="不同意" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                       </asp:Panel> 
                                     </table>  
                             </ContentTemplate>
                    </asp:TabPanel>
                    </asp:TabContainer>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

