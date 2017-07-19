<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_YFDIF.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YFDIF" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>运费差额</strong>
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
<div class="box-inner">
                  <div class="box_right">
                     <div class="box-title">
                        <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Button ID="btndc" runat="server" Text="按任务号导出" OnClick="btndc_click" />
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="hledit" runat="server" CssClass="hand">
                                    <asp:Image ID="Image4" ImageUrl="~/Assets/icons/move_arrange.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" OnClick="viewCondition()" />筛选条件</asp:HyperLink>
                                <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                    Position="Bottom" Enabled="true" runat="server" OffsetX="-600" OffsetY="-30" TargetControlID="hledit"
                                    PopupControlID="PanelCondition" EnableViewState="true">
                                </asp:PopupControlExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                        </tr>
                    </table>
                   <asp:Panel ID="PanelCondition" runat="server" Width="500px" Style="display: none">
                        <table width="500px" style="background-color: #CCCCFF; border: solid 1px black;">
                            <tr>
                                <td colspan="4" align="right">
                               
                                    
                                    <asp:Button ID="btnsearch" runat="server" Text="查 询" OnClick="btnsearch_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重 置" OnClick="btnReset_Click" />
                                     &nbsp;&nbsp;&nbsp;
                                    <input id="ipClose" runat="server" type="button" onclick="document.body.click(); return false;"
                                        value="取 消" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="100px">
                                    起始年月(yyyy-mm)：
                                </td>
                                <td align="left">
                                    <asp:TextBox id="startdat" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="startdat">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right" width="100px">
                                    结束年月(yyyy-mm)：
                                </td>
                                <td align="left">
                                    <asp:TextBox id="enddat" runat="server" type="text"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="enddat">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                        CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="名称">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                    <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="DIFYF_JSDH">结算单号</asp:ListItem>
                                                        <asp:ListItem Value="DIFYF_BJID">外协件编号</asp:ListItem>
                                                        <asp:ListItem Value="DIFYF_BJNAME">外协件名称</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="比较关系">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                    <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                        <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                        <asp:ListItem Value="1">等于</asp:ListItem>
                                                        <asp:ListItem Value="2">不等于</asp:ListItem>
                                                        <asp:ListItem Value="3">大于</asp:ListItem>
                                                        <asp:ListItem Value="4">大于或等于</asp:ListItem>
                                                        <asp:ListItem Value="5">小于</asp:ListItem>
                                                        <asp:ListItem Value="6">小于或等于</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数值">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="逻辑">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                    <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                        <asp:ListItem Value="OR" Selected="True">或者</asp:ListItem>
                                                        <asp:ListItem Value="AND">并且</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                           
                        </table>
                    </asp:Panel>
                   
                    </div>
                  </div>
                </div>
<asp:Panel ID="Panel1" runat="server">
<div class="box-inner">
   <div style=" overflow:scroll">
          <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
          
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                        <th>
                        序号
                        </th>
                        <th>
                        任务号
                        </th>
                         <th>
                        年度
                        </th>
                        <th>
                        会计期间
                        </th>
                        <th>
                        结算单日期
                        </th>
                        <th>
                        结算单号
                        </th>
                        <th>
                        计划跟踪号
                        </th>
                        <th>
                        设备名称
                        </th>
                        <th>
                        图号
                        </th>
                        <th>
                        结算单原金额
                        </th>
                        <th>
                        发票金额
                        </th>
                        <th>
                        差额
                        </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <td>
                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbrwh" runat="server" Enabled="false" Text='<%#Eval("DIFYF_TSAID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbyear" runat="server" Enabled="false" Text='<%#Eval("DIFYF_YEAR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbmonth" runat="server" Enabled="false" Text='<%#Eval("DIFYF_MONTH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbjsddate" runat="server" Enabled="false" Text='<%#Eval("DIFYF_JSDDATE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbjsdh" runat="server" Enabled="false" Text='<%#Eval("DIFYF_JSDH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbjhgzh" runat="server" Enabled="false" Text='<%#Eval("DIFYF_JHGZH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsbmc" runat="server" Enabled="false" Text='<%#Eval("DIFYF_BJNAME")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsbbh" runat="server" Enabled="false" Text='<%#Eval("DIFYF_BJID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbjsdje" runat="server" Enabled="false" Text='<%#Eval("DIFYF_JSDYJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbfpje" runat="server" Enabled="false" Text='<%#Eval("DIFYF_FPJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbce" runat="server" Enabled="false" Text='<%#Eval("DIFYF_DIFMONEY")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="3" align="right">
                            合计:
                            </th>
                            <th colspan="6">
                            
                            </th>
                            <th>
                               <asp:Label ID="lbjsdjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                 <asp:Label ID="lbfpjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                 <asp:Label ID="lbcehj" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                没有记录！</asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            </div>
 </div>
         </asp:Panel>
</asp:Content>
