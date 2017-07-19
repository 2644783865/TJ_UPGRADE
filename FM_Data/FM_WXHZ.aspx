<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_WXHZ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_WXHZ" Title="外协汇总" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>外协汇总</strong>
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
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
<div class="box-inner">
                  <div class="box_right">
                     <div class="box-title">
                        <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                <asp:HyperLink ID="hypwxzgcx" runat="server" NavigateUrl="FM_WXZGCX.aspx"><asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />外协暂估查询</asp:HyperLink>
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
                                <td colspan="4" align="right"><strong>是否勾稽：</strong>
                                    <asp:DropDownList ID="DropDownifgj" runat="server" Width="60px">
                                                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="0">否</asp:ListItem>
                                                        <asp:ListItem Value="3">是</asp:ListItem>
                                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <asp:TextBox id="startdate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="startdate">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right" width="100px">
                                    结束年月(yyyy-mm)：
                                </td>
                                <td align="left">
                                    <asp:TextBox id="enddate" runat="server" type="text"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="enddate">
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
                                                        <asp:ListItem Value="TAHZ_JSDH">结算单号</asp:ListItem>
                                                        <asp:ListItem Value="TAHZ_WXJBH">外协件编号</asp:ListItem>
                                                        <asp:ListItem Value="TZHZ_WXJNAME">外协件名称</asp:ListItem>
                                                        <asp:ListItem Value="TAHZ_TSAID">任务号</asp:ListItem>
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
                        结算单号
                        </th>
                        <th>
                        任务号
                        </th>
                        <th>
                        年月
                        </th>
                        <th>
                        数量
                        </th>
                        <th>
                        金额
                        </th>
                        <th>
                        含税金额
                        </th>
                        <th>
                        外协件编号
                        </th>
                        <th>
                        外协件名称
                        </th>
                        <th>
                        材质
                        </th>
                        <th>
                        规格
                        </th>
                        <th>
                        勾稽状态
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
                                <asp:Label ID="lbjsdh" runat="server" Enabled="false" Text='<%#Eval("TAHZ_JSDH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbrwh" runat="server" Enabled="false" Text='<%#Eval("TAHZ_TSAID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbyearmonth" runat="server" Enabled="false" Text='<%#Eval("TAHZ_YEARMONTH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbsl" runat="server" Enabled="false" Text='<%#Eval("TAHZ_NUM")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbmoney" runat="server" Enabled="false" Text='<%#Eval("TAHZ_MONEY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsmoney" runat="server" Enabled="false" Text='<%#Eval("TAHZ_HSMONEY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwxjbh" runat="server" Enabled="false" Text='<%#Eval("TAHZ_WXJBH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwxjname" runat="server" Enabled="false" Text='<%#Eval("TZHZ_WXJNAME")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbcaizhi" runat="server" Enabled="false" Text='<%#Eval("TAHZ_CAIZHI")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbguige" runat="server" Enabled="false" Text='<%#Eval("TAHZ_GUIGE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbgjzt" runat="server" Enabled="false" Text='<%#Eval("TAHZ_GJSTATE")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="2" align="right">
                            合计:
                            </th>
                            <th colspan="3">
                            
                            </th>
                            <th>
                               <asp:Label ID="lbjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                 <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                            </th>
                            <th>
                            
                            </th>
                            <th>
                            
                            </th>
                            <th>
                            
                            </th>
                            <th>
                            
                            </th>
                            <th>
                            
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
</asp:Content>
