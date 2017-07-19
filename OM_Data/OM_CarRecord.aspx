<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarRecord.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarRecord" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆使用记录&nbsp;&nbsp;&nbsp;
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
            background: #EFF3FB;
            z-index: 2;
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
            overflow-y: hidden;
            overflow-x: scroll;
        }
    </style>

    <script type="text/javascript" src="~/JS/jquery/jquery-1.3.1.js"></script>

    <script type="text/javascript">
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

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
             <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 10%; height: 24px">
                            <b>用车记录信息</b>
                        </td>
                        <td colspan="4">
                            <strong>发车日期:</strong> 从<asp:TextBox ID="txtStartTime" runat="server" class="easyui-datetimebox"
                                data-options="editable:false" Width="80px"></asp:TextBox>
                            到<asp:TextBox ID="txtEndTime" runat="server" class="easyui-datetimebox" data-options="editable:false"
                                Width="80px"></asp:TextBox>
                        </td>
                        <%--<td align="left">
                  <asp:DropDownList ID="ddl_Year" runat="server" OnSelectedIndexChanged="ddl_Year_OnSelectedIndexChanged" />&nbsp;年&nbsp;
                  <asp:DropDownList ID="ddl_Month" runat="server" OnSelectedIndexChanged="ddl_Month_OnSelectedIndexChanged" />&nbsp;月&nbsp;</td>--%>
                        <td align="right">
                            <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_change">
                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                <asp:ListItem Text="车牌号" Value="CARNUM"></asp:ListItem>
                                <asp:ListItem Text="司机" Value="SJNAME"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlcontent" runat="server" Width="100px">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtSearch" runat="server" onfocus="this.select();" onchange="GetContent(this);" Width="130px"></asp:TextBox>--%>
                            <asp:Button ID="btnSearch" CssClass="buttOut" runat="server" Text=" 查 询 " OnClick="btnSearch_OnClick" />
                            <asp:Button ID="btn_reset" CssClass="buttOut" runat="server" Text="重 置" OnClick="btn_reset_OnClick" />
                        </td>
                        <td style="text-align: right" width="200px">
                            <%--<asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_CarRecordAdd.aspx?action=add" runat="server">
                      <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2" ImageAlign="AbsMiddle" runat="server" />
                        新增车辆记录
                  </asp:HyperLink>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="grid">
        <div id="gridview">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
            <asp:GridView ID="GridView1" Width="100%" CssClass="datagrid" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <HeaderStyle Wrap="false" />
                <RowStyle Wrap="false" BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CARNUM" HeaderText="车牌号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="YDTIME" HeaderText="始发时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TIME2" HeaderText="结束时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="WHOLETIME" HeaderText="用时" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SFPLACE" HeaderText="始发地" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DESTINATION" HeaderText="目的地" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LICHENG1" HeaderText="始发里程/千米" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LICHENG2" HeaderText="结束里程/千米" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="WHOLEJULI" HeaderText="路程/千米" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SJNAME" HeaderText="司机" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="APPLYER" HeaderText="申请人" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="SJNOTE" HeaderText="司机备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NOTE" HeaderText="申请备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <%--<PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />--%>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" Height="24px" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
           
        </div>
    </div>
     <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                 <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <table>
                <tr align="center">
                    <td align="center">
                        筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text="" ForeColor="Red"></asp:Label>条记录&nbsp;&nbsp;合计用时：<asp:Label
                            ID="lb_oil" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                        合计里程：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>千米
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;
            <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
