<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_CBTZD.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_CBTZD" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>
    <script src="../JS/DatePicker.js" type="text/javascript"></script>
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
    <table width="100%">
        <tr>
            
            
        </tr>
    </table>
    

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            成本调整单
                        </td>
                        <td>
                            类型：
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_OnClick">
                        <asp:ListItem Text="入库成本调整单" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="出库成本调整单" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="期初成本调整单" Value="2" ></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" Y="30">
                            </asp:ModalPopupExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_export" runat="server" Text="导 出" OnClientClick="return verifyExport()"
                                OnClick="btn_export_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="400px" Style="display: none">
                    <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="480px" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td>
                                       时间：从
                                    </td>
                                    <td>
                                      <asp:TextBox ID="beg_time" runat="server"></asp:TextBox>
                                         <asp:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                            TargetControlID="beg_time">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                      到
                                    </td>
                                    <td>
                                      <asp:TextBox ID="end_time" runat="server"></asp:TextBox>
                                         <asp:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                            TargetControlID="end_time">
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
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数值">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlTypeValue" runat="server" Width="128px" Style="display: none">
                                                        </asp:DropDownList>
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
            <asp:UpdatePanel ID="UpdatePanelBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <HeaderTemplate>
                            <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid"
                                border="1" width="100%">
                                <tr align="center" class="tableTitle">
                                    <td>
                                        行号
                                    </td>
                                    
                                    <td>
                                        年度
                                    </td>
                                    <td>
                                        会计期间
                                    </td>
                                    <td>
                                        物料编码
                                    </td>
                                    <td>
                                        物料名称
                                    </td>
                                    <td>
                                        材质
                                    </td>
                                    <td>
                                        规格
                                    </td>
                                   
                                    <td>
                                        差额
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight' " onmouseout="this.className='baseGadget'" >
                            
                                <td>
                                    <asp:Label ID="LblmonthMar" runat="server" Text='<%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>'></asp:Label>
                                </td>
                                <td>
                                    <%# Eval("SI_YEAR")%>
                                </td>
                                <td>
                                    <%# Eval("SI_PERIOD")%>
                                </td>
                                <td>
                                    <%#Eval("SI_MARID")%>
                                </td>
                                <td>
                                    <%#Eval("MNAME")%>
                                </td>
                                
                                <td>
                                    <%#Eval("CAIZHI")%>
                                </td>
                                <td>
                                    <%#Eval("GUIGE")%>
                                </td>
                                
                                                              
                                <td>
                                    <%# Eval("DIFF")%>
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
                        <asp:Label ID="LabelDIFF" runat="server"></asp:Label>
                        </td>
                        
                    </tr>
                    </table>
                </FooterTemplate>
                        
                    </asp:Repeater>
            <br />
                    <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <asp:HiddenField ID="hfdDIFF" runat="server" />
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
        </div>
    </div>
</asp:Content>
