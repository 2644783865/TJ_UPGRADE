<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_YFFromJSD_ToInv.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YFFromJSD_ToInv"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    运费结算单信息
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function viewCondition(){
   document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
}
    </script>

    <script language="javascript" type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#table1 input[type=checkbox]").attr("checked","true");
}
else{
 $("#table1 input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            全选/取消<input id="Checkbox2" type="checkbox" />
                        </td>
                        <td>
                            <strong>每页显示记录条数</strong>
                            <asp:DropDownList ID="DropDownListCount" runat="server" Width="128px" OnSelectedIndexChanged="Count_Change"
                                AutoPostBack="true">
                                <asp:ListItem Value="20" Selected="True" Text="">20</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right">
                            <asp:HyperLink ID="hledit" runat="server" CssClass="hand">
                                <asp:Image ID="Image4" ImageUrl="~/Assets/icons/move_arrange.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" OnClick="viewCondition()" />筛选条件</asp:HyperLink>
                            <asp:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                Position="Bottom" Enabled="true" runat="server" OffsetX="-600" OffsetY="-30"
                                TargetControlID="hledit" PopupControlID="PanelCondition" EnableViewState="true">
                            </asp:PopupControlExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;<%--筛选--%>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="下推发票" OnClick="btnXTFP_Click" />
                        </td>
                        <td align="right" style="width: 10%">
                            <a href="FM_YFInvoice_Managemnt.aspx" title="返回到运费发票管理界面">返回</a> &nbsp;&nbsp;&nbsp;&nbsp;
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
                                起始时间：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="startdat" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                    TargetControlID="startdat">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right" width="100px">
                                结束时间：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="enddat" runat="server" type="text"></asp:TextBox>
                                <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
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
                                                    <asp:ListItem Value="JS_BH">结算单号</asp:ListItem>
                                                    <asp:ListItem Value="JS_GYS">供应商</asp:ListItem>
                                                    <asp:ListItem Value="JS_RWH">任务号</asp:ListItem>
                                                    <asp:ListItem Value="JS_HTH">合同号</asp:ListItem>
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
    <div class="box-outer">
        <div style="overflow: scroll" align="center">
            <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                border="1">
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center">
                            <td align="center">
                                <strong>序号</strong>
                            </td>
                            <td align="center">
                                <strong>结算单号</strong>
                            </td>
                            <td align="center" style="background-color:#98FB98">
                                <strong>日期</strong>
                            </td>
                            <td align="center">
                                <strong>供应商名称</strong>
                            </td>
                            <td align="center">
                                <strong>制单人姓名</strong>
                            </td>
                            <td align="center">
                                <strong>计划跟踪号</strong>
                            </td>
                            <td align="center">
                                <strong>任务号</strong>
                            </td>
                            <td align="center">
                                <strong>图号</strong>
                            </td>
                            <td align="center">
                                <strong>设备名称</strong>
                            </td>
                            <td align="center">
                                <strong>数量</strong>
                            </td>
                            <td align="center">
                                <strong>单重</strong>
                            </td>
                            <td align="center">
                                <strong>金额</strong>
                            </td>
                            <td align="center">
                                <strong>税率</strong>
                            </td>
                            <td align="center">
                                <strong>含税金额</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <td>
                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                    Checked="false" Onclick="checkme(this)"></asp:CheckBox>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbjsdh" runat="server" Text='<%#Eval("JS_BH")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center" style="background-color:#98FB98">
                                <asp:Label ID="lbdate" runat="server" Text='<%#Eval("JS_RQ")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbgysmc" runat="server" Text='<%#Eval("JS_GYS")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbzdrxm" runat="server" Text='<%#Eval("JS_ZDR")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbjhgzh" runat="server" Text='<%#Eval("JS_JHGZH")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("JS_RWH")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbtuhao" runat="server" Text='<%#Eval("JS_TUHAO")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbmname" runat="server" Text='<%#Eval("JS_SBMC")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbsl" runat="server" Text='<%#Eval("JS_BJSL")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbzl" runat="server" Text='<%#Eval("JS_DANZ")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbje" runat="server" Text='<%#Eval("MONEY")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbshuilv" runat="server" Text='<%#Eval("JS_SHUIL")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="lbhsje" runat="server" Text='<%#Eval("JS_HSJE")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="2" align="right">
                                合计:
                            </th>
                            <th colspan="8">
                            </th>
                            <th>
                                <asp:Label ID="lbzlhj" runat="server"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lbjehj" runat="server"></asp:Label>
                            </th>
                            <th>
                            </th>
                            <th align="center">
                                <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red">
                没有记录！</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
