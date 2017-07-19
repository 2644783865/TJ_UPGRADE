<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Paint_Scheme_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Paint_Scheme_View" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    油漆涂装细化方案    
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script type="text/javascript" language="javascript">
     function exportMSExcel()
     {
        var date=new Date();
        var nouse=date.getDate();
        var obj=new Object();
        window.showModalDialog("TM_PS_ExprotExcel.aspx?action=<%=GetTaskID %>&time="+nouse,obj,"dialogHeight:250px;dialogWidth:450px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
     }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>  
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="90%">
            <tr>
                <td style="width:16%"><b>涂装方案查看</b></td>
                <td align="right">状态:</td>
                <td>
                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="6" runat="server" 
                        AutoPostBack="true" onselectedindexchanged="rblstatus_SelectedIndexChanged">                
                    </asp:RadioButtonList>
                </td>
                <td align="center">
                    <asp:LinkButton ID="lnkbtnNew" runat="server" OnClick="lnkbtnNew_OnClick">新建油漆方案<asp:Image ID="ImageTo" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2" align="absmiddle" runat="server" /></asp:LinkButton>
                </td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1" CssClass="hand"  onclick="exportMSExcel();" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:HyperLink>
                </td>
           </tr>
        </table>
    </div>
    </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PS_ID" HeaderText="批号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PS_PJID" HeaderText="合同号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="TSA_ENGNAME" HeaderText="设备名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PS_SUBMITTM" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="PS_ADATE" HeaderText="批准日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lab_state" runat="server" 
                            Text='<%# Eval("PS_STATE").ToString()=="0"||Eval("PS_STATE").ToString()=="1"?"未提交":Eval("PS_STATE").ToString()=="2"?"待审核":Eval("PS_STATE").ToString()=="4"||Eval("PS_STATE").ToString()=="6"?"审核中...":Eval("PS_STATE").ToString()=="8"?"通过":Eval("PS_STATE").ToString()=="9"?"已处理":"驳回" %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlDetail" CssClass="link" 
                         NavigateUrl='<%# "TM_Paint_Scheme_Create.aspx?id="+Eval("PS_NO")+"&edit="+Eval("PS_ENGID") %>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />查看                                
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEdit" CssClass="link"  NavigateUrl='<%# "TM_Paint_Scheme_Create.aspx?pseditid="+Eval("PS_NO")+"&edit="+Eval("PS_ENGID") %>' runat="server">
                        <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />编辑                                
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                      <ItemTemplate>
                          <asp:LinkButton ID="lkbtnCancel" runat="server" OnClientClick='return confirm("确认将该批作废吗？");' OnClick="lkbtnCancel_OnClick" CommandArgument='<%#Eval("PS_ID") %>'> <asp:Image ID="Image5" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />作废</asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                      <ItemTemplate>
                          <asp:LinkButton ID="lkbtnDelete" runat="server" OnClientClick="return confirm('确认删除该批吗？');" OnClick="lkbtnDelete_OnClick" CommandArgument='<%#Eval("PS_ID") %>'> <asp:Image ID="Image6" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />删除</asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
