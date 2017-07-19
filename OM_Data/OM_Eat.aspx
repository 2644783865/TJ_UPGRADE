<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_Eat.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Eat" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用餐申请及审批&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script language="javascript" type="text/javascript">
//    function chkchange(obj)
//    {
////    console.log(obj.checked);
//if(obj.checked)

//    {
//       $(obj).parent().parent().css("background","red");
//       $("#<%=fache.ClientID %>").show();
//     }
//       else
//     {
//        $(obj).parent().parent().css("background","white");
//       $("#<%=fache.ClientID %>").hide();
//     }
//    }
 $(function()
 {
 $("#<%=gridview1.ClientID %> tr").mouseover(function()
 {
if($(this).find(":checked").length==0)
{ $(this).css("background","#C8F7FF");}

 });
 
  $("#<%=gridview1.ClientID %> tr").mouseout(function()
  {
  if($(this).find(":checked").length==0)
  {$(this).css("background","white");}
 
 });
});
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <%--<asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>--%>
                <table width="100%">
                    <tr>
                        <td style="width: 60px">
                            审批状态:
                        </td>
                        <td align="left" style="width: 750px">
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="8" runat="server" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:Button ID="fache" runat="server" hidden="hidden" Text="食堂--接收" OnClick="fache_click"
                                OnClientClick="return confirm('确认接收吗?')"></asp:Button>
                        </td>
                        <td align="right">
                            <%--<asp:Button ID="btnApply" runat="server" Text="新增用餐申请" OnClientClick="add()" />--%>
                            <%--<asp:Button ID="add" runat="server" Text="添加" OnClientClick="add()" />--%>
                            <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_EatApplyDetail.aspx?action=add"
                                runat="server" Visible="false">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                    ImageAlign="AbsMiddle" runat="server" />
                                新增用餐申请
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 100%; overflow: auto">
            <asp:GridView ID="gridview1" runat="server" CssClass="toptable" OnRowDataBound="gridview1_change"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="chk" runat="server" type="checkbox" disabled="disabled"
                                name="chkname" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="xh" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="state" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="2"?"待接收":Eval("STATE").ToString()=="4"?"已接收":Eval("STATE").ToString()=="6"?"已反馈/待确认":Eval("STATE").ToString()=="8"?"已确认":Eval("STATE").ToString()=="9"?"已取消":"驳回" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BUMENNAME" HeaderText="用餐部门" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PEOPLENUM" HeaderText="用餐人数" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PEOPLEGUIGE" HeaderText="用餐规格" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="ALLMONEY" HeaderText="用餐总金额" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <%--<asp:BoundField DataField="YCTYPE" HeaderText="用餐类型" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />--%>
                    <asp:TemplateField HeaderText="用餐类型" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="yclx" runat="server" Text='<%# Eval("YCTYPE").ToString()=="1"?"加班餐":Eval("YCTYPE").ToString()=="2"?"客饭":"" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请人" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyer" runat="server" Text='<%#Eval("APPLYNAME") %>'></asp:Label>
                            <asp:Label ID="lblApplyerID" runat="server" Visible="false" Text='<%#Eval("APPLYID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="APPLYPHONE" HeaderText="申请人电话" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="USETIME" HeaderText="用餐时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    
                    <%-- <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="state" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"待审中":Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="2"||Eval("STATE").ToString()=="4"?"审核中...":Eval("STATE").ToString()=="8"?"通过":Eval("STATE").ToString()=="9"?"已取消":"驳回" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="LBL_STATE" runat="server" Text='<%# Eval("STATE")%>' Visible="false"></asp:Label>
                            <asp:HyperLink ID="hplmod" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_EatApplyDetail.aspx?action=mod&id="+Eval("CODE") %>'>
                                <asp:Image ID="image1" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                <asp:Label ID="xiugai" runat="server" Text='<%# Eval("STATE").ToString()=="1"||Eval("STATE").ToString()=="3"||Eval("STATE").ToString()=="5"||Eval("STATE").ToString()=="7"?"驳回处理":"修改" %>' />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplview" runat="server" CssClass="link" NavigateUrl='<%#"OM_EatApplyDetail.aspx?action=view&id="+Eval("CODE") %>'>
                                <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                <asp:label id="lab_audit_view" Text="查看"  runat="server" />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="食堂反馈" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlpfk" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_EatApplyDetail.aspx?action=addjl&id="+Eval("CODE") %>'>
                                <asp:Image ID="imagefk" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                食堂反馈
                            </asp:HyperLink>
                            <asp:HyperLink ID="hlpfkck" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_EatApplyDetail.aspx?action=viewjl&id="+Eval("CODE") %>'>
                                <asp:Image ID="imagefkck" runat="server" border="0" hspace="2" align="absmiddle"
                                    ImageUrl="~/Assets/images/create.gif" />
                                反馈查看
                            </asp:HyperLink>
                            <asp:HyperLink ID="hlpfkxg" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_EatApplyDetail.aspx?action=modjl&id="+Eval("CODE") %>'>
                                <asp:Image ID="imagefkxg" runat="server" border="0" hspace="2" align="absmiddle"
                                    ImageUrl="~/Assets/images/create.gif" />
                                反馈修改
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="是否发车" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="fache" runat="server" Text='<%# Eval("FACHE").ToString()=="0"?"未发车":"已发车" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="车是否回" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="HUICHE" runat="server" Text='<%# Eval("HUICHE").ToString()=="0"?"未回":"已回" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="用餐确认" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlpqr" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_EatApplyDetail.aspx?action=viewjl&id="+Eval("CODE") %>'>
                                <asp:Image ID="imageqr" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                用餐确认
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false" />
                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" OnClick="lnkDelete_OnClick"
                                CommandArgument='<%# Eval("CODE")%>' CommandName="SHANCHU" OnClientClick="return confirm('确认删除吗?')">
                                <asp:Image ID="image4" ImageUrl="~/Assets/images/create.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />
                                删除
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="取消" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="quxiao" runat="server" Visible="false" OnClick="cancel_change"
                                CommandArgument='<%# Eval("CODE")%>' CommandName="quxiao" OnClientClick="return confirm('确认取消吗?')">
                            取消
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>--%>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <table id="yanse" runat="server">
                <tr align="left">
                    <td style="background-color: Orange">
                        橘红：部门待审批
                    </td>
                    <td style="background-color:  Red">
                        红色：食堂审核
                    </td>
                    <td style="background-color: Green">
                        绿色：食堂反馈
                    </td>
                    <td style="background-color: Lime">
                        亮绿：部门用餐确认
                    </td>
                    <%--<td style="background-color: Brown">
                        棕色：部门不确认
                    </td>--%>
                    <%--<td style="background-color: Gray;">
                        灰色：已取消
                    </td>--%>
                    <td style="background-color: Gray">
                        灰色：驳回
                    </td>
                    <td style="background-color: White">
                        无色：已完成
                    </td>
                </tr>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
