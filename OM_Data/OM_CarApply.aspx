<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarApply.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarApply" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用车申请及审批&nbsp;&nbsp;&nbsp;
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
            overflow-y: hidden;
            overflow-x: scroll;
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

              var newTr=$('<tr style="white-space:normal; height:24px;"></tr>');    /*这里tr的样式也一样，但要注意的是，有时gridview会隔一行换一种样式，这里的JS代码需要做一些处理*/

              newTr.append($(tr).children(".fixed").clone());

              t.append(newTr);

         }

         d.append(t);

    });
    
    
    function add()
    {
        window.location.href = 'OM_CarApplyDetail.aspx?action=add'
//        var sRet = window.showModalDialog('OM_CarApplyDetail.aspx?action=add','obj','dialogWidth=900px;dialogHeight=600px');
//        if (sRet == "refresh") 
//        {
//            window.location.href = window.location.href;
//        }
    }

    function chkchange(obj)
    {
//    if(obj.checked)
//    {
//    obj.parentNode.parentNode.style.backgroundColor="Red";
//    }

if(obj.checked)

    {
       $(obj).parent().parent().css("background","red");
       $("#<%=ddlcarnum.ClientID %>").show();
       }
       else{
        $(obj).parent().parent().css("background","white");
       $("#<%=ddlcarnum.ClientID %>").hide();
       }
    }
 $(function(){
 $("#<%=gridview1.ClientID %> tr").mouseover(function(){
if($(this).find(":checked").length==0){ $(this).css("background","#C8F7FF");}

 });
 
  $("#<%=gridview1.ClientID %> tr").mouseout(function(){
  if($(this).find(":checked").length==0){$(this).css("background","white");}
 
 });
 });
 
 $(function(){
 $("#<%=ddlcarnum.ClientID %>").change(function(){
 if($(this).val()!="-请选择-")
 {
 $("#<%=fache.ClientID %>").show();
 } 
 else
 {
 $("#<%=fache.ClientID %>").hide();
 }
// alert("aa");
 });
 });
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
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
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlcarnum" runat="server" hidden="hidden" Width="120px">
                            </asp:DropDownList>
                            <asp:Button ID="fache" runat="server" hidden="hidden" Text="发车--反馈" OnClick="fache_click"
                                OnClientClick="return confirm('确认发车吗?')"></asp:Button>
                        </td>
                        <td align="right">
                            <a href='OM_CarApplyDetail.aspx?action=add'>新增用车申请</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="grid">
        <div id="gridview">
            <asp:GridView ID="gridview1" runat="server" CssClass="datagrid" OnRowDataBound="gridview1_change"
                Style="white-space: normal" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chk" runat="server" type="checkbox" disabled="disabled" onchange="chkchange(this);"
                                name="chkname" />
                            <input type="hidden" id="hidState" value='<% #Eval("STATE") %>' runat="server" />
                            <input type="hidden" id="hidFaChe" value='<% #Eval("FACHE") %>' runat="server" />
                             <input type="hidden" id="hidHuiChe" value='<% #Eval("HUICHE") %>' runat="server" />
                                <input type="hidden" id="hidSJID" value='<% #Eval("SJID") %>' runat="server" />
                                   <input type="hidden" id="hidApplyerId" value='<% #Eval("APPLYERID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="主键" ItemStyle-HorizontalAlign="Center" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lbid" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="state" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"?"审核中...":Eval("STATE").ToString()=="8"?"通过":Eval("STATE").ToString()=="9"?"已取消":"驳回" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="车牌号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="CARNUM" runat="server" Text='<%#Eval("CARNUM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DEPARTMENT" HeaderText="用车部门" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:TemplateField HeaderText="外出事由" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtWAICHU" Text='<%# Eval("REASON")%>' runat="server" Width="100px"
                                ToolTip='<%# Eval("REASON")%>' ReadOnly="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="REASON" HeaderText="外出事由" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />--%>
                    <asp:BoundField DataField="SFPLACE" HeaderText="始发地" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <%-- <asp:BoundField DataField="DESTINATION" HeaderText="目的地" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" />--%>
                    <asp:TemplateField HeaderText="目的地" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMUDIDI" Text='<%# Eval("DESTINATION")%>' runat="server" Width="100px"
                                ToolTip='<%# Eval("DESTINATION")%>' ReadOnly="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NUM" HeaderText="乘车人数" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyer" runat="server" Text='<%#Eval("APPLYER") %>'></asp:Label>
                            <asp:Label ID="lblApplyerID" runat="server" Visible="false" Text='<%#Eval("APPLYERID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PHONE" HeaderText="申请人电话" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="YDTIME" HeaderText="发车时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="SJNAME" HeaderText="司机" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="SJCALL" HeaderText="司机电话" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="USETIME1" HeaderText="用车时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="USETIME2" HeaderText="申请用车结束时间" Visible="false" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="TIME1" HeaderText="实际开始时间" Visible="false" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="TIME2" HeaderText="结束时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <%--<asp:BoundField DataField="NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />--%>
                    <%-- <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="state" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"?"审核中...":Eval("STATE").ToString()=="8"?"通过":Eval("STATE").ToString()=="9"?"已取消":"驳回" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplmod" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_CarApplyDetail.aspx?action=mod&id="+Eval("CODE") %>'>
                                <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                <asp:Label ID="xiugai" runat="server" Text='<%# Eval("STATE").ToString()=="3"||Eval("STATE").ToString()=="5"||Eval("STATE").ToString()=="7"?"驳回处理":"修改" %>' />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplview" runat="server" CssClass="link" NavigateUrl='<%#"OM_CarApplyDetail.aspx?action=view&diff=look&id="+Eval("CODE") %>'>
                                <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                查看
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否发车" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="fache" runat="server" Text='<%# Eval("FACHE").ToString()=="0"?"未发车":"已发车" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="车是否回" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="HUICHE" runat="server" Text='<%# Eval("HUICHE").ToString()=="0"?"未回":"已回" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="车辆回厂" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="link" runat="server" Visible="false" OnClick="link_change" CommandArgument='<%# Eval("ID")%>'
                                CommandName="back" OnClientClick="return confirm('确认回厂吗?')">
                            回厂
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" OnClick="lnkDelete_OnClick"
                                CommandArgument='<%# Eval("CODE")%>' CommandName="SHANCHU" OnClientClick="return confirm('确认删除吗?')">
                                <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />
                                删除
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="取消" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="quxiao" runat="server" Visible="false" OnClick="cancel_change"
                                CommandArgument='<%# Eval("CODE")%>' CommandName="quxiao" OnClientClick="return confirm('确认取消吗?')">
                            取消
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                <table id="yanse" style="position: relative;" runat="server">
                    <tr align="left">
                        <td style="background-color: Red">
                            红色：待办
                        </td>
                        <td style="background-color: Yellow;">
                            黄色：审核中
                        </td>
                        <td style="background-color: Gray;">
                            灰色：已取消
                        </td>
                        <td style="background-color: DarkBlue;">
                            深蓝：驳回
                        </td>
                        <td style="background-color: White;">
                            无色：通过
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                    没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
