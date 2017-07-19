<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarWeihu.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarWeihu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆详细信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <%-- <script language="javascript" type="text/javascript">
    function add1()
    {
//    debugger;
    var carnum=document.getElementById('<%=lblCarNum.ClientID%>').value;
        var sRet = window.showModalDialog("OM_CarAddSafe.aspx?action=add&flag="+carnum+"",'obj','dialogWidth=600px;dialogHeight=350px');
        if (sRet == "refresh") 
        {
            window.location.href = window.location.href;
        }
    }
    function add2() 
    {
//    debugger;
    var carnum=document.getElementById('<%=lblCarNum.ClientID%>').Value;
    alert(carnum);
    var sRet = window.showModalDialog("OM_CarAddOil.aspx?action=add&flag="+carnum+"",'obj','dialogWidth=600px;dialogHeight=350px');
        if (sRet == "refresh") {
            window.location.href = window.location.href;
        }
    }
    
    $(function()
    {
    $("#<%=btn2.ClientID %>").click(
    function()
    {
    var carnum=$("#<%=lblCarNum.ClientID %>").val();
    var sRet = window.showModalDialog("OM_CarAddOil.aspx?action=add&flag="+carnum+"",'obj','dialogWidth=600px;dialogHeight=350px');
        if (sRet == "refresh") {
            window.location.href = window.location.href;
        }
    });
    });
    </script>--%>
    <%--<div class="box-inner">
      <div class="box_right">
        <div class="box-title">
          <table style="width:100%; height:24px">
            <tr>
              <td style="width:10%; height:24px"><b>用车记录信息</b></td>
              
              <td align="left">
                  <asp:DropDownList ID="ddl_Year" runat="server" OnSelectedIndexChanged="ddl_Year_OnSelectedIndexChanged" />&nbsp;年&nbsp;
                  <asp:DropDownList ID="ddl_Month" runat="server" OnSelectedIndexChanged="ddl_Month_OnSelectedIndexChanged" />&nbsp;月&nbsp;</td>
              <td align="right">
                  <asp:DropDownList ID="ddlSearch" runat="server" OnSelectedIndexChanged="ddlSearch_OnSelectedIndexChanged">
                      <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                      <asp:ListItem Text="车牌号" Value="CARNUM"></asp:ListItem>
                      <asp:ListItem Text="司机" Value="DRIVER"></asp:ListItem>
                  </asp:DropDownList></td>            
              <td align="left"> 
                  <asp:TextBox ID="txtSearch" runat="server" onfocus="this.select();" onchange="GetContent(this);" Width="130px"></asp:TextBox>
                  <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " OnClick="btnSearch_OnClick" />
                  </td>--%>
    <%--<td style="text-align: right" width="200px">
                  <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_CarRecordAdd.aspx?action=add" runat="server">
                      <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2" ImageAlign="AbsMiddle" runat="server" />
                        新增车辆记录
                  </asp:HyperLink>
              </td>--%>
    <%-- </tr> </table> </div> </div> </div>--%>
    <%-- <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_OnClick" CausesValidation="False" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>--%>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">--%>
    <contenttemplate>
            <div class="RightContent">
                <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top" 
                    ActiveTabIndex="0">
                    <cc1:TabPanel ID="Tab_clwh" runat="server" HeaderText="车辆维修记录" TabIndex="0" Width="100%">
                        <ContentTemplate>
                            <div class="box-inner">
                             <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                                <div class="box_right">
                                    <div class="box-title">
                                        <table style="width: 100%; height: 24px">
                                            <tr>
                                                <td style="width: 10%; height: 24px">
                                                    <b>维修记录信息</b>
                                                </td>
                                                <td colspan="4"><strong>维修日期:</strong>                  
                    从<asp:TextBox ID="txtStartTime" runat="server" class="easyui-datebox"   data-options="editable:false"  Width="80px"></asp:TextBox>
                    到<asp:TextBox ID="txtEndTime" runat="server" class="easyui-datebox" data-options="editable:false"  Width="80px"></asp:TextBox>
              </td> 
                                                <%--<td align="left">
                                                    <asp:DropDownList ID="ddl_Year" runat="server" OnSelectedIndexChanged="ddl_Year_OnSelectedIndexChanged" />
                                                    &nbsp;年&nbsp;
                                                    <asp:DropDownList ID="ddl_Month" runat="server" OnSelectedIndexChanged="ddl_Month_OnSelectedIndexChanged" />
                                                    &nbsp;月&nbsp;
                                                </td>--%>
                                                <td align="right">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" >
                                                        <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="车牌号" Value="CARID"></asp:ListItem>
                                                        <asp:ListItem Text="经办人" Value="APPLYNAME"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSearch" runat="server"  onfocus="this.select();" onchange="GetContent(this);"
                                                        Width="130px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server"  CssClass="buttOut"  Text=" 查 询 " OnClick="btnSearch_OnClick" />
                                                     <asp:Button ID="btn_reset" CssClass="buttOut" runat="server" Text="重 置" OnClick="btn_reset_OnClick"/> 
                                                </td>
                                                <td style="text-align: right" width="300px">
                                                 <asp:DropDownList ID="ddl_CODE" runat="server" ></asp:DropDownList>
                                                 <asp:Button ID="bt_add1" runat="server" Text="新增维修记录" OnClick="add1_click" />
                                                   <%-- <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_CarAddSafe.aspx?action=add"
                                                        runat="server">
                                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                                            ImageAlign="AbsMiddle" runat="server" />
                                                           
                                                        新增记录
                                                    </asp:HyperLink>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <asp:GridView ID="gridview1" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" OnRowDataBound="gridview1_databound">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="CARTYPE" runat="server" Text='<%#Eval("CARNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="车牌号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="CARNUM" runat="server" Text='<%#Eval("CARID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GOODSNAME" HeaderText="物品名称" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="GOODSCOUNT" HeaderText="物品数量" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="GOODSUNIT" HeaderText="物品单位" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="GOODSPRICE" HeaderText="物品单价" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="MONEYONE" HeaderText="小计" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="MONEYALL" HeaderText="总计" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="经办人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApplyer" runat="server" Text='<%#Eval("APPLYNAME") %>'></asp:Label>
                                        <asp:Label ID="lblApplyerID" runat="server" Visible="false" Text='<%#Eval("APPLYID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            
                                           <asp:BoundField DataField="PLACE" HeaderText="维修地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PLACEDATE" HeaderText="维修时间" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="DATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplmod" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_CARWXSQ_Detail.aspx?action=mod&id="+Eval("CODE") %>'>
                                            <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            <%--<asp:Label ID="xiugai" runat="server" Text='<%# Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="3"?"驳回处理":"修改" %>' />--%>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" OnClick="lnkDelete_OnClick"
                                            CommandArgument='<%# Eval("CODE")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                            <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0"
                                                hspace="2" align="absmiddle" />
                                            删除
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                        没有记录!</asp:Panel>
                                        <td align="center">筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text="" ForeColor="Red"></asp:Label>条记录&nbsp;&nbsp;
    合计维护费用：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    &nbsp;&nbsp;
   
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Tab_clBY" runat="server" HeaderText="车辆保养记录"  TabIndex="0" Width="100%">
                        <ContentTemplate>
                            <div class="box-inner">
                                <div class="box_right">
                                    <div class="box-title">
                                        <table style="width: 100%; height: 24px">
                                            <tr>
                                                <td style="width: 10%; height: 24px">
                                                    <b>保养记录信息</b>
                                                </td>
                                                <td colspan="4"><strong>保养日期:</strong>                  
                    从<asp:TextBox ID="txtStartTime3" runat="server" class="easyui-datebox"   data-options="editable:false"  Width="80px"></asp:TextBox>
                    到<asp:TextBox ID="txtEndTime3" runat="server" class="easyui-datebox" data-options="editable:false"  Width="80px"></asp:TextBox>
              </td> 
                                                <%--<td align="left">
                                                    <asp:DropDownList ID="ddl_Year" runat="server" OnSelectedIndexChanged="ddl_Year_OnSelectedIndexChanged" />
                                                    &nbsp;年&nbsp;
                                                    <asp:DropDownList ID="ddl_Month" runat="server" OnSelectedIndexChanged="ddl_Month_OnSelectedIndexChanged" />
                                                    &nbsp;月&nbsp;
                                                </td>--%>
                                                <td align="right">
                                                    <asp:DropDownList ID="ddlSearch3" runat="server" >
                                                        <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="车牌号" Value="CARID"></asp:ListItem>
                                                        <asp:ListItem Text="经办人" Value="APPLYNAME"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSearch3" runat="server"  onfocus="this.select();" onchange="GetContent(this);"
                                                        Width="130px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch3" runat="server"  CssClass="buttOut"  Text=" 查 询 " OnClick="btnSearch3_OnClick" />
                                                     <asp:Button ID="btn_reset3" CssClass="buttOut" runat="server" Text="重 置" OnClick="btn_reset3_OnClick"/> 
                                                </td>
                                                <td style="text-align: right" width="300px">
                                                 <asp:DropDownList ID="ddl_CODE3" runat="server" ></asp:DropDownList>
                                                 <asp:Button ID="bt_add3" runat="server" Text="新增保养记录" OnClick="add3_click" />
                                                   <%-- <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_CarAddSafe.aspx?action=add"
                                                        runat="server">
                                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                                            ImageAlign="AbsMiddle" runat="server" />
                                                           
                                                        新增记录
                                                    </asp:HyperLink>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <asp:GridView ID="gridview3" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" OnRowDataBound="gridview3_databound">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="CARTYPE" runat="server" Text='<%#Eval("CARNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="车牌号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="CARNUM" runat="server" Text='<%#Eval("CARID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GOODSNAME" HeaderText="物品名称" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="GOODSCOUNT" HeaderText="物品数量" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="GOODSUNIT" HeaderText="物品单位" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="GOODSPRICE" HeaderText="物品单价" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="MONEYONE" HeaderText="小计" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="MONEYALL" HeaderText="总计" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="经办人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApplyer" runat="server" Text='<%#Eval("APPLYNAME") %>'></asp:Label>
                                        <asp:Label ID="lblApplyerID" runat="server" Visible="false" Text='<%#Eval("APPLYID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            
                                           <asp:BoundField DataField="PLACE" HeaderText="保养地点" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PLACEDATE" HeaderText="保养时间" HeaderStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="DATE" HeaderText="制单日期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" />
                                <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplmod" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_CARWXSQ_Detail.aspx?action=mod&id="+Eval("CODE") %>'>
                                            <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            <%--<asp:Label ID="xiugai" runat="server" Text='<%# Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="3"?"驳回处理":"修改" %>' />--%>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" OnClick="lnkDelete_OnClick"
                                            CommandArgument='<%# Eval("CODE")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                            <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0"
                                                hspace="2" align="absmiddle" />
                                            删除
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <asp:Panel ID="NoDataPanel3" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                        没有记录!</asp:Panel>
                                        <td align="center">筛选结果：共<asp:Label ID="lb_select_num3" runat="server" Text="" ForeColor="Red"></asp:Label>条记录&nbsp;&nbsp;
    合计维护费用：<asp:Label ID="lb_select_money3" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    &nbsp;&nbsp;
   
                                    <uc1:UCPaging ID="UCPaging3" runat="server" />
                                    
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Tab_oil" runat="server" HeaderText="车辆加油记录" TabIndex="1" Width="100%">
                        <ContentTemplate>
                        
                         <div class="box-inner">
                                <div class="box_right">
                                    <div class="box-title">
                                        <table style="width: 100%; height: 24px">
                                            <tr>
                                                <td style="width: 10%; height: 24px">
                                                    <b>用车记录信息</b>
                                                </td>
                                                <td colspan="4"><strong>维护日期:</strong>                  
                    从<asp:TextBox ID="txtStartTime2" runat="server" class="easyui-datebox" data-options="editable:false"  Width="80px"></asp:TextBox>
                    到<asp:TextBox ID="txtEndTime2" runat="server" class="easyui-datebox" data-options="editable:false"  Width="80px"></asp:TextBox>
              </td> 
                                                <%--<td align="left">
                                                    <asp:DropDownList ID="ddl_Year" runat="server" OnSelectedIndexChanged="ddl_Year_OnSelectedIndexChanged" />
                                                    &nbsp;年&nbsp;
                                                    <asp:DropDownList ID="ddl_Month" runat="server" OnSelectedIndexChanged="ddl_Month_OnSelectedIndexChanged" />
                                                    &nbsp;月&nbsp;
                                                </td>--%>
                                                <td align="right">
                                                    <asp:DropDownList ID="ddlSearch2" runat="server" >
                                                        <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="车牌号" Value="CARNUM"></asp:ListItem>
                                                        <asp:ListItem Text="司机" Value="DRIVER"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSearch2" runat="server" onfocus="this.select();" onchange="GetContent(this);"
                                                        Width="130px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch2" runat="server"  CssClass="buttOut"  Text=" 查 询 " OnClick="btnSearch2_OnClick" />
                                                     <asp:Button ID="btn_reset2" CssClass="buttOut" runat="server" Text="重 置" OnClick="btn_reset2_OnClick"/> 
                                                </td>
                                                <td style="text-align: right" width="200px">
                                                    <asp:HyperLink ID="hlAdd2" CssClass="link" NavigateUrl="~/OM_Data/OM_CarAddOil.aspx?action=add"
                                                        runat="server">
                                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                                            ImageAlign="AbsMiddle" runat="server" />
                                                        新增加油记录
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    
                                    <asp:GridView ID="gridview2" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" OnRowDataBound="gridview2_databound">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="车牌号" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCarNum" runat="server" Text='<%#Eval("CARNUM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RQ" HeaderText="日期">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DRIVER" HeaderText="司机">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="加油方式" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                            <asp:Label ID="state3" runat="server" Text='<%# Eval("TYPE").ToString()=="cash"?"现金":"加油卡" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            <asp:BoundField DataField="CARDID" HeaderText="加油卡号">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARDYE" HeaderText="加油卡余额:元">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OILTYPE" HeaderText="油型号">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OILWEAR" HeaderText="油耗:升/百公里">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARLICHENG" HeaderText="加油前公里数:公里">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UPRICE" HeaderText="单价:元/升" DataFormatString="{0:C2}" HtmlEncode="False">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OILNUM" HeaderText="加油量:升">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MONEY" HeaderText="花费:元" DataFormatString="{0:C2}" HtmlEncode="False">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOTE" HeaderText="备注">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                                                ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                                        hspace="2" align="absmiddle" />
                                                    <asp:LinkButton ID="lnkDelete2" runat="server" CommandArgument='<%# Eval("ID")%>'
                                                        OnClick="lnkDelete2_OnClick" CommandName="Del" OnClientClick="return confirm('确认删除吗?')">删除</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                        没有记录!</asp:Panel>
                                         <td align="center">筛选结果：共<asp:Label ID="lb_select_num2" runat="server" Text="" ForeColor="Red"></asp:Label>条记录&nbsp;&nbsp;合计油量：<asp:Label ID="lb_oil" runat="server" Text="" ForeColor="Red"></asp:Label>升（L）&nbsp;&nbsp;
    合计费用：<asp:Label ID="lb_select_money2" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    &nbsp;&nbsp;
                                    <uc1:UCPaging ID="UCPaging2" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                     <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="加油卡消费/充值记录" TabIndex="1" Width="100%">
                        <ContentTemplate>
                        
                         <div class="box-inner">
                                <div class="box_right">
                                    <div class="box-title">
                                        <table style="width: 100%; height: 24px">
                                            <tr>
                                                <td style="width: 10%; height: 24px">
                                                    <b>记录信息</b>
                                                </td>
                                                <td colspan="4"><strong>维护日期:</strong>                  
                    从<asp:TextBox ID="txtStartTime4" runat="server" class="easyui-datebox" data-options="editable:false"  Width="80px"></asp:TextBox>
                    到<asp:TextBox ID="txtEndTime4" runat="server" class="easyui-datebox" data-options="editable:false"  Width="80px"></asp:TextBox>
              </td> 
                                                <%--<td align="left">
                                                    <asp:DropDownList ID="ddl_Year" runat="server" OnSelectedIndexChanged="ddl_Year_OnSelectedIndexChanged" />
                                                    &nbsp;年&nbsp;
                                                    <asp:DropDownList ID="ddl_Month" runat="server" OnSelectedIndexChanged="ddl_Month_OnSelectedIndexChanged" />
                                                    &nbsp;月&nbsp;
                                                </td>--%>
                                                <td align="right">
                                                    <asp:DropDownList ID="ddlSearch4" runat="server" >
                                                        <asp:ListItem Text="车牌号" Value="CARNUM"></asp:ListItem>
                                                        <asp:ListItem Text="司机" Value="DRIVER"></asp:ListItem>
                                                        <asp:ListItem Text="卡号" Value="CARDID" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSearch4" runat="server" onfocus="this.select();" onchange="GetContent(this);"
                                                        Width="130px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch4" runat="server"  CssClass="buttOut"  Text=" 查 询 " OnClick="btnSearch4_OnClick" />
                                                     <asp:Button ID="btn_reset4" CssClass="buttOut" runat="server" Text="重 置" OnClick="btn_reset4_OnClick"/> 
                                                </td>
                                                <td style="text-align: right" width="200px">
                                                    <asp:HyperLink ID="HyperLink1" CssClass="link" NavigateUrl="~/OM_Data/OM_CarAddMoney.aspx?action=add"
                                                        runat="server">
                                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                                            ImageAlign="AbsMiddle" runat="server" />
                                                        新增充值记录
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    
                                    <asp:GridView ID="gridview4" runat="server" Width="100%" CssClass="toptable" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" OnRowDataBound="gridview2_databound">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
<%--                                            <asp:TemplateField HeaderText="车牌号" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCarNum" runat="server" Text='<%#Eval("CARNUM") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="RQ" HeaderText="日期">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DRIVER" HeaderText="司机">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="CARDID" HeaderText="加油卡号">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARDBYE" HeaderText="充值前余额:元（￥）">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARDCZ" HeaderText="充值金额:元（￥）">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARDYE" HeaderText="加油卡现余额:元（￥）">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>
                                            
                                     <%--       <asp:BoundField DataField="NOTE" HeaderText="备注">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundField>--%>
                                           
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <asp:Panel ID="NoDataPanel4" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                        没有记录!</asp:Panel>
                                         <td align="center">筛选结果：共<asp:Label ID="lb_select_num4" runat="server" Text="" ForeColor="Red"></asp:Label>条记录&nbsp;&nbsp;&nbsp;&nbsp;
    合计充值金额：<asp:Label ID="lb_select_money4" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    &nbsp;&nbsp;
                                    <uc1:UCPaging ID="UCPaging4" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </contenttemplate>
    <%--    </asp:UpdatePanel>--%>
</asp:Content>
