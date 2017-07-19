<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_KuCunYuEChaXun.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_KuCunYuEChaXun"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    function viewCondition()
    {
     document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
    }
    function setFlag()
    {
        document.getElementById("<%=CurType.ClientID%>").value="yes";
    }
   function verifyExport()
   {
     var extype = document.getElementById("<%=CurType.ClientID%>").value;     
     if(extype!="yes")
     {
        alert("请选择筛选条件查看");
        return false;
     }else
     {
        return true;
     }
   }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            库存余额查询
                        </td>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" Y="30">
                            </asp:ModalPopupExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="导 出" OnClientClick="return verifyExport()"
                                OnClick="btn_export_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               时间： 
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            <asp:Button ID="btn_update" runat="server" Text="更新本月收发存汇总" OnClick="btn_update_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="700px" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="700px" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td width="80px">
                                        物料级别：
                                    </td>
                                    <td height="23px" align="left">
                                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                            <asp:ListItem Text="一级" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="二级" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="三级" Value="03" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        年/月：
                                    </td>
                                    <td height="23px">
                                        <asp:RadioButtonList ID="rbl1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="month" Selected="True">月</asp:ListItem>
                                            <asp:ListItem Value="year">年</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right" height="23px">
                                        从年月<asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>
                                         <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="txtStartTime">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td align="center">
                                       到年月<asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>
                                         <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM"
                                            TargetControlID="txtEndTime">
                                        </asp:CalendarExtender> 
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                            BorderWidth="1px" OnDataBound="GridViewSearch_DataBound">
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="名称">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="比较关系">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                                            <asp:ListItem Value="1">等于</asp:ListItem>
                                                            <asp:ListItem Value="2">不等于</asp:ListItem>
                                                            <asp:ListItem Value="3">大于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于或等于</asp:ListItem>
                                                            <asp:ListItem Value="5">小于</asp:ListItem>
                                                            <asp:ListItem Value="6">小于或等于</asp:ListItem>
                                                            <asp:ListItem Value="7">不包含</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数值">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                        <%--<asp:DropDownList ID="ddlTypeValue" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="逻辑">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
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
                                <tr>
                                    <td colspan="5" align="center" height="23px">
                                        <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " OnClientClick="setFlag()" OnClick="btnQuery_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="取 消" OnClick="btnClose_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:HiddenField ID="hfdBEGBAL" runat="server" />
            <asp:HiddenField ID="hfdENDBAL" runat="server" />
            <asp:HiddenField ID="hfdCRCVMNY" runat="server" />
            <asp:HiddenField ID="hfdCSNDMNY" runat="server" />
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="rpt_monthMar" runat="server" OnItemDataBound="rpt_monthMar_ItemDataBound">
                        <HeaderTemplate>
                            <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid"
                                border="1" width="100%">
                                <tr align="center" class="tableTitle">
                                    <td rowspan="2">
                                        行号
                                    </td>
                                    <td rowspan="2">
                                        物料编码
                                    </td>
                                    <td rowspan="2">
                                        物料名称
                                    </td>
                                    <td rowspan="2">
                                        材质
                                    </td>
                                    <td rowspan="2">
                                        规格
                                    </td>
                                    <td rowspan="2">
                                        国标
                                    </td>
                                    <td rowspan="2">
                                        单位
                                    </td>
                                    <td colspan="2">
                                        日期
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        期初
                                    </td>
                                    <td colspan="4" style="height: 20px">
                                        本期
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        期末
                                    </td>
                                </tr>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        年度
                                    </td>
                                    <td>
                                        会计期间
                                    </td>
                                    <td style="height: 20px">
                                        期初数量
                                    </td>
                                    <td style="height: 20px">
                                        期初金额
                                    </td>
                                    <td style="height: 20px">
                                        收入数量
                                    </td>
                                    <td style="height: 20px">
                                        收入金额
                                    </td>
                                    <td style="height: 20px">
                                        发出数量
                                    </td>
                                    <td style="height: 20px">
                                        发出金额
                                    </td>
                                    <td style="height: 20px">
                                        期末数量
                                    </td>
                                    <td style="height: 20px">
                                        期末金额
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight' " onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="LblmonthMar" runat="server" Text='<%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("SI_MARID")%>
                                </td>
                                <td>
                                    <%#Eval("SI_MARNM")%>
                                </td>
                                
                                <td>
                                    <%#Eval("SI_CAIZHI")%>
                                </td>
                                <td>
                                    <%#Eval("SI_GUIGE")%>
                                </td>
                                <td>
                                    <%#Eval("GB")%>
                                </td>
                                <td>
                                    <%#Eval("PURCUNIT")%>
                                </td>
                                
                                <td>
                                    <%# Eval("SI_YEAR")%>
                                </td>
                                <td>
                                    <%# Eval("SI_PERIOD")%>
                                </td>
                                <td>
                                    <%# Eval("SI_BEGNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_BEGBAL")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CRCVNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CRCVMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CSNDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CSNDMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_ENDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_ENDBAL")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="baseGadget">
                                <td>
                                </td>
                                <td>
                                    合计:
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelBEGBAL" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCRCVMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCSNDMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelENDBAL" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rpt_yearMar" runat="server" OnItemDataBound="rpt_yearMar_ItemDataBound">
                        <HeaderTemplate>
                            <table id="table2" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                                border="1" width="100%">
                                <tr align="center" class="tableTitle">
                                    <td rowspan="2">
                                        行号
                                    </td>
                                    <td rowspan="2">
                                        物料编码
                                    </td>
                                    <td rowspan="2">
                                        物料名称
                                    </td>
                                  
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        年初
                                    </td>
                                    <td colspan="4" style="height: 20px">
                                        本期
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        年末
                                    </td>
                                </tr>
                                <tr align="center" class="tableTitle">
                                    <td style="height: 20px">
                                        年度
                                    </td>
                                    <td style="height: 20px">
                                        年初数量
                                    </td>
                                    <td style="height: 20px">
                                        年初金额
                                    </td>
                                    <td style="height: 20px">
                                        收入数量
                                    </td>
                                    <td style="height: 20px">
                                        收入金额
                                    </td>
                                    <td style="height: 20px">
                                        发出数量
                                    </td>
                                    <td style="height: 20px">
                                        发出金额
                                    </td>
                                    <td style="height: 20px">
                                        年末数量
                                    </td>
                                    <td style="height: 20px">
                                        年末金额
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="LblyearMar" runat="server" Text='<%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("SI_MARID")%>
                                </td>
                                <td>
                                    <%#Eval("SI_MARNM")%>
                                </td>           
                                <td>
                                    <%# Eval("SI_YEAR")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YBEGNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YBEGBAL")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YRCVNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YRCVMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSNDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSNDMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSENDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSENDMNY")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="baseGadget">
                                <td>
                                </td>
                                <td>
                                    合计:
                                </td>
                                <td>
                                </td>                                
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelBEGBAL" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCRCVMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCSNDMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelENDBAL" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rpt_monthType" runat="server" OnItemDataBound="rpt_monthType_ItemDataBound">
                        <HeaderTemplate>
                            <table id="table3" align="center" cellpadding="2" cellspacing="1" class="toptable grid"
                                border="1" width="100%">
                                <tr align="center" class="tableTitle">
                                    <td rowspan="2">
                                        行号
                                    </td>
                                    <td rowspan="2">
                                        类别编码
                                    </td>
                                    <td rowspan="2">
                                        类别名称
                                    </td>
                                    <td colspan="2">
                                        日期
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        期初
                                    </td>
                                    <td colspan="4" style="height: 20px">
                                        本期
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        期末
                                    </td>
                                </tr>
                                <tr align="left" class="tableTitle">
                                    <td>
                                        年度
                                    </td>
                                    <td>
                                        会计期间
                                    </td>
                                    <td style="height: 20px">
                                        期初数量
                                    </td>
                                    <td style="height: 20px">
                                        期初金额
                                    </td>
                                    <td style="height: 20px">
                                        收入数量
                                    </td>
                                    <td style="height: 20px">
                                        收入金额
                                    </td>
                                    <td style="height: 20px">
                                        发出数量
                                    </td>
                                    <td style="height: 20px">
                                        发出金额
                                    </td>
                                    <td style="height: 20px">
                                        期末数量
                                    </td>
                                    <td style="height: 20px">
                                        期末金额
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight' " onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="LblmonthType" runat="server" Text='<%#Container.ItemIndex +1%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("SI_TYPEID")%>
                                </td>
                                <td>
                                    <%#Eval("SI_TYNAME")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YEAR")%>
                                </td>
                                <td>
                                    <%# Eval("SI_PERIOD")%>
                                </td>
                                <td>
                                    <%# Eval("SI_BEGNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_BEGBAL")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CRCVNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CRCVMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CSNDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_CSNDMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_ENDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_ENDBAL")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="baseGadget">
                                <td>
                                </td>
                                <td>
                                    合计:
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>                                
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelBEGBAL" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCRCVMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCSNDMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelENDBAL" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rpt_yearType" runat="server" OnItemDataBound="rpt_yearType_ItemDataBound">
                        <HeaderTemplate>
                            <table id="table4" align="center" cellpadding="2" cellspacing="1" class="toptable grid"
                                border="1" width="100%">
                                <tr align="center" class="tableTitle">
                                    <td rowspan="2">
                                        行号
                                    </td>
                                    <td rowspan="2">
                                        类别编码
                                    </td>
                                    <td rowspan="2">
                                        类别名称
                                    </td>
                                    <td colspan="1">
                                        日期
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        年初
                                    </td>
                                    <td colspan="4" style="height: 20px">
                                        本年
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        年末
                                    </td>
                                </tr>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        年度
                                    </td>
                                    <td style="height: 20px">
                                        年初数量
                                    </td>
                                    <td style="height: 20px">
                                        年初金额
                                    </td>
                                    <td style="height: 20px">
                                        收入数量
                                    </td>
                                    <td style="height: 20px">
                                        收入金额
                                    </td>
                                    <td style="height: 20px">
                                        发出数量
                                    </td>
                                    <td style="height: 20px">
                                        发出金额
                                    </td>
                                    <td style="height: 20px">
                                        年末数量
                                    </td>
                                    <td style="height: 20px">
                                        年末金额
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight' " onmouseout="this.className='baseGadget'">
                                <td>
                                    <asp:Label ID="LblyearType" runat="server" Text='<%#Container.ItemIndex + 1%>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("SI_TYPE")%>
                                </td>
                                <td>
                                    <%#Eval("SI_TYNAME")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YEAR")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YBEGNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YBEGBAL")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YRCVNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YRCVMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSNDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSNDMNY")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSENDNUM")%>
                                </td>
                                <td>
                                    <%# Eval("SI_YSENDMNY")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                        
                         <tr class="baseGadget">
                                <td>
                                </td>
                                <td>
                                    合计:
                                </td>
                                <td>
                                </td>                                
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelBEGBAL" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCRCVMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelCSNDMNY" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelENDBAL" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <asp:TextBox ID="CurType" runat="server" Text="" Style="display: none"></asp:TextBox>
                <div>
                </div>
            </div>
            <%--  </ContentTemplate>
         </asp:UpdatePanel>
       <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
        <ProgressTemplate>
               <div style="position: absolute; top: 30%; right:45%">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
               </div>--%>
            <%--        </ProgressTemplate>
        </asp:UpdateProgress>   --%>
</asp:Content>
