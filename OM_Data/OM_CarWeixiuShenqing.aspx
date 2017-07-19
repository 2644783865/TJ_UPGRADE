<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_CarWeixiuShenqing.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.OM_Data.OM_CarWeixiuShenqing" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆维修//保养申请&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .datagrid td
        {
            padding: 5px;
            height: 24px;
            text-align: center;
        }
        .fixed
        {
            background-color: #EFF3FB;
            z-index: 0;
        }
        .gridWidth
        {
            width: 4000px;
        }
        .unfixed
        {
            border: none;
        }
        #grid
        {
            position: relative;
            display: block;
            width: 100%;
            margin: 0px auto;
            background: #fff;
            overflow: hidden;
        }
        #gridview
        {
            position: relative;
            display: block;
            width: 100%;
            background: #fff;
        }
    </style>

    <script type="text/javascript" src="~/JS/jquery/jquery-1.3.1.js"></script>

    <script language="javascript" type="text/javascript">
    
     $(document).ready(function(){

    var d=$("<div></div>");

    var height=0;

    var width=0;

    d.css({position:"absolute",left:"0",top:"0",background:"#fff"});

         $("#grid").append(d);
          

         var view=$(".datagrid");

         var t=$('<table class="datagrid" cellspacing="0" rules="all" border="1"  style="font-size:9pt; height:24px;border-collapse:collapse;"></table>');    /*这里的table样式是在浏览器中查看GridView解析后的表格的样式，直接复制就行！*/

         var trlist=view.find("tr");

         for(var i=0;i<trlist.length;i++){

              var tr=trlist[i];

              var newTr=$('<tr style="white-space:nowrap; height:24px;"></tr>');    /*这里tr的样式也一样，但要注意的是，有时gridview会隔一行换一种样式，这里的JS代码需要做一些处理*/

              newTr.append($(tr).children(".fixed").clone());

              t.append(newTr);

         }

         d.append(t);
         


    });
    
    
    
//    function add()
//    {
//        var sRet = window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=add','obj','dialogWidth=900px;dialogHeight=600px');
//        if (sRet == "refresh") 
//        {
//            window.location.href = window.location.href;
//        }
//    }
window.onload=function(){
 $("#<%=gridview1.ClientID %> tr").mouseover(function(){
$(this).css("background","#C8F7FF");}
 );
 
  $("#<%=gridview1.ClientID %> tr").mouseout(function(){
$(this).css("background","white");}
);

 $("#<%=gridview2.ClientID %> tr").mouseover(function(){
$(this).css("background","#C8F7FF");}
 );
 
  $("#<%=gridview2.ClientID %> tr").mouseout(function(){
$(this).css("background","white");}
);
}
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0">
        <cc1:TabPanel ID="Tab_clwh" runat="server" HeaderText="车辆维修申请" TabIndex="0" Width="100%">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                            <%--<asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>--%>
                            <table width="100%">
                                <tr>
                                    <td style="width: 60px">
                                        审批状态:
                                    </td>
                                    <td align="center" style="width: 311px">
                                        <asp:RadioButtonList ID="rblstatus" RepeatColumns="5" runat="server" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="驳回" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="通过" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="我的审核任务" Value="4" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right">
                                        <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_CARWXSQ_Detail.aspx?action=add&type=wx"
                                            runat="server">
                                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" runat="server" />
                                            新增维修申请
                                        </asp:HyperLink>
                                        <%--<asp:Button ID="btnApply" runat="server" Text="新增维修申请单" OnClientClick="add()" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="grid" style="width:100%;overflow:auto">
                    <div id="gridview">
                        <asp:GridView ID="gridview1" runat="server" CssClass="datagrid" OnRowDataBound="gridview1_change"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Wrap="false" />
                            <RowStyle Wrap="false" BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <input id="chk" runat="server" type="checkbox" disabled="disabled" name="chkname" />
                                        <%--<asp:CheckBox ID="CheckBox1" Enabled="false" runat="server"  OnCheckedChanged="CheckBox1_CheckedChanged" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApplyer" runat="server" Text='<%#Eval("APPLYNAME") %>'></asp:Label>
                                        <asp:Label ID="lblApplyerID" runat="server" Visible="false" Text='<%#Eval("APPLYID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="司机调度" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManage" runat="server" Text='<%#Eval("MANAGERNAME") %>'></asp:Label>
                                        <asp:Label ID="lblManageID" runat="server" Visible="false" Text='<%#Eval("MANAGERID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调度意见" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="state1" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="1"?"未通过":Eval("STATE").ToString()=="2"?"通过":"--" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="综合负责人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControl" runat="server" Text='<%#Eval("CONTROLLERNAME") %>'></asp:Label>
                                        <asp:Label ID="lblControlID" runat="server" Visible="false" Text='<%#Eval("CONTROLLERID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="负责人意见" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="state2" runat="server" Text='<%# Eval("STATE").ToString()=="2"?"待审中":Eval("STATE").ToString()=="3"?"未通过":Eval("STATE").ToString()=="4"?"通过":"--" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审核//查看" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask1" CssClass="link" NavigateUrl='<%#"OM_CARWXSQ_Detail.aspx?action=view&type=wx&id="+Eval("CODE")%>'
                                            runat="server">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            <asp:Label ID="state3" runat="server" Text='<%# Eval("STATE").ToString()=="0"||Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"?"审核//查看":"查看" %>'></asp:Label>
                                        </asp:HyperLink>
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
                                        <asp:HyperLink ID="hplmod" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_CARWXSQ_Detail.aspx?action=mod&type=wx&id="+Eval("CODE") %>'>
                                            <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            <asp:Label ID="xiugai" runat="server" Text='<%# Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="3"?"驳回处理":"修改" %>' />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" OnClick="lnkDelete_OnClick"
                                            CommandArgument='<%# Eval("CODE")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                            <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0"
                                                hspace="2" align="absmiddle" />
                                            删除
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                没有记录!</asp:Panel>
                          
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="车辆保养申请" TabIndex="0" Width="100%">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <%--<asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>--%>
                            <table width="100%">
                                <tr>
                                    <td style="width: 60px">
                                        审批状态:
                                    </td>
                                    <td align="center" style="width: 311px">
                                        <asp:RadioButtonList ID="rblstatus2" RepeatColumns="5" runat="server" OnSelectedIndexChanged="rblstatus2_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="驳回" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="通过" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="我的审核任务" Value="4" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right">
                                        <asp:HyperLink ID="HyperLink1" CssClass="link" NavigateUrl="~/OM_Data/OM_CARWXSQ_Detail.aspx?action=add&type=by"
                                            runat="server">
                                            <asp:Image ID="Image3" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" runat="server" />
                                            新增保养申请
                                        </asp:HyperLink>
                                        <%--<asp:Button ID="btnApply" runat="server" Text="新增维修申请单" OnClientClick="add()" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="gridtwo" style="width:100%;overflow:auto">
                    <div id="gridviewtwo">
                        <asp:GridView ID="gridview2" runat="server" CssClass="datagrid" OnRowDataBound="gridview2_change"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <input id="chk2" runat="server" type="checkbox" disabled="disabled" name="chkname" />
                                        <%--<asp:CheckBox ID="CheckBox1" Enabled="false" runat="server"  OnCheckedChanged="CheckBox1_CheckedChanged" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="xh2" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode2" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="CARTYPE2" runat="server" Text='<%#Eval("CARNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="车牌号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="CARNUM2" runat="server" Text='<%#Eval("CARID") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApplyer2" runat="server" Text='<%#Eval("APPLYNAME") %>'></asp:Label>
                                        <asp:Label ID="lblApplyerID2" runat="server" Visible="false" Text='<%#Eval("APPLYID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="司机调度" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManage2" runat="server" Text='<%#Eval("MANAGERNAME") %>'></asp:Label>
                                        <asp:Label ID="lblManageID2" runat="server" Visible="false" Text='<%#Eval("MANAGERID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调度意见" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="state12" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="1"?"未通过":Eval("STATE").ToString()=="2"?"通过":"--" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="综合负责人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControl2" runat="server" Text='<%#Eval("CONTROLLERNAME") %>'></asp:Label>
                                        <asp:Label ID="lblControlID2" runat="server" Visible="false" Text='<%#Eval("CONTROLLERID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="负责人意见" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="state22" runat="server" Text='<%# Eval("STATE").ToString()=="2"?"待审中":Eval("STATE").ToString()=="3"?"未通过":Eval("STATE").ToString()=="4"?"通过":"--" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="审核//查看" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask1" CssClass="link" NavigateUrl='<%#"OM_CARWXSQ_Detail.aspx?action=view&type=wx&id="+Eval("CODE")%>'
                                            runat="server">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            <asp:Label ID="state32" runat="server" Text='<%# Eval("STATE").ToString()=="0"||Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"?"审核//查看":"查看" %>'></asp:Label>
                                        </asp:HyperLink>
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
                                        <asp:HyperLink ID="hplmod2" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_CARWXSQ_Detail.aspx?action=mod&type=by&id="+Eval("CODE") %>'>
                                            <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                            <asp:Label ID="xiugai2" runat="server" Text='<%# Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="3"?"驳回处理":"修改" %>' />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                                    ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete2" Visible="false" runat="server" OnClick="lnkDelete2_OnClick"
                                            CommandArgument='<%# Eval("CODE")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                            <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0"
                                                hspace="2" align="absmiddle" />
                                            删除
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" ForeColor="Red">
                                没有记录!</asp:Panel>
                           
                            <uc1:UCPaging ID="UCPaging2" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
