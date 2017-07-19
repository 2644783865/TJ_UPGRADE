<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_Inspection_Manage.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Inspection_Manage"
    Title="我的报检任务" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    我的报检任务</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">

 function ShowViewModal(ID) {
     var date=new Date();
        var time=date.getTime();
        var retVal = window.open("QC_Inspection_Add.aspx?ACTION=VIEW&&back=1&&id="+ID);
    }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table style="width: 98%; height: 24px">
                    <tr>
                        <td align="center" width="20%">
                            <asp:RadioButtonList ID="RadioButtonListTask" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Value="all">全部</asp:ListItem>
                                <asp:ListItem Value="self" Selected="True">我的任务</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right" width="10%">
                            状态:
                        </td>
                        <td width="20%">
                            <asp:RadioButtonList ID="rblstatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="0" Selected="True">未质检</asp:ListItem>
                                <asp:ListItem Value="1">已质检</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" width="20%">
                          <asp:RadioButtonList ID="RadioButtonListResult" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged" Visible="false" >
                                <asp:ListItem Value="不合格" >不合格</asp:ListItem>
                                <asp:ListItem Value="合格">合格</asp:ListItem>
                                <asp:ListItem Value="all">全部</asp:ListItem>
                            </asp:RadioButtonList>  
                        </td>
                        <td align="left" width="10%">
                            <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                        </td>
                        <td align="right" width="20%">
                            <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/QC_Data/QC_Inspection_Add_Blank.aspx?ACTION=NEW"
                                runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" Width="15px" />
                                添加质检单
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="padding-right: 8px; padding-left: 8px">
            <div style="width: 100%; overflow-x: auto; overflow-y: hidden; padding-bottom: 20px;">
                <div style="width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                项目名称：<asp:TextBox ID="TextBoxProj" runat="server"></asp:TextBox>
                                 <asp:AutoCompleteExtender ID="xmmc_AutoCompleteExtender" runat="server" 
                                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                    MinimumPrefixLength="1" ServiceMethod="xmmc" ServicePath="~/Ajax.asmx" 
                                    TargetControlID="TextBoxProj" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                生产制号：<asp:TextBox ID="TextBoxENGID" runat="server"></asp:TextBox>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                报检部门：
                                <asp:DropDownList ID="DropDownListDep" runat="server" AutoPostBack="true" 
                                    onselectedindexchanged="DropDownListDep_SelectedIndexChanged">
                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                    <asp:ListItem Value="06">采购部</asp:ListItem>
                                    <asp:ListItem Value="07">储运部</asp:ListItem>
                                    <asp:ListItem Value="03">技术部</asp:ListItem>
                                    <asp:ListItem Value="04">生产部</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                报检人：<asp:DropDownList ID="DropDownListInspecMan" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                质检人：<asp:TextBox ID="TextBoxMan" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                工程名称：<asp:TextBox ID="TextBoxEng" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="gcmc_AutoCompleteExtender" runat="server" 
                                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                                    MinimumPrefixLength="1" ServiceMethod="gcmc" ServicePath="~/Ajax.asmx" 
                                    TargetControlID="TextBoxEng" UseContextKey="True"></asp:AutoCompleteExtender>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                部件名称：<asp:TextBox ID="TextBoxBJMC" runat="server"></asp:TextBox>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                报检日期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox><asp:CalendarExtender
                                    ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="TextBoxDate">
                                </asp:CalendarExtender>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                供货商：<asp:TextBox ID="TextBoxSUPPLERNM" runat="server"></asp:TextBox>
                            </td>
                            <td style="white-space: nowrap; width: 20%;" align="left">
                                质检编号：<asp:TextBox ID="TextBoxZJID" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" DataKeyNames="AFI_ID" OnDataBound="GridView1_DataBound"
                    OnRowDataBound="GridView1_RowDataBound">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                </asp:CheckBox>
                                <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="质检编号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbafiid" runat="server" Text='<%# Eval("AFI_ID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbafisj" runat="server" Text='<%# Eval("AFI_DATE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbzjid" runat="server"></asp:Label>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AFI_ENGID" HeaderText="生产制号" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_PJNAME" HeaderText="项目" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_ENGNAME" HeaderText="工程名称" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_PARTNAME" HeaderText="部件名称" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_TSDEP" HeaderText="部门" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_MANNM" HeaderText="报检人" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_DATE" HeaderText="报检时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_RQSTCDATE" HeaderText="需要检测时间" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_ISPCTSITE" HeaderText="检测地点" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="报检次数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lb_state" runat="server" Text='<%#Eval("AFI_NUMBER")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="AFI_QCMANNM" HeaderText="质检人" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFI_ENDDATE" HeaderText="质检时间" HeaderStyle-Wrap="false"
                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="结果" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lb_result" runat="server" ForeColor='<%#Eval("AFI_ENDRESLUT").ToString()=="不合格"?System.Drawing.Color.Red:Eval("AFI_ENDRESLUT").ToString()=="合格"?System.Drawing.Color.Blue:System.Drawing.Color.White%>'
                                    Text='<%#Eval("AFI_ENDRESLUT")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="修改/报检" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Panel ID="PanelUpdate" runat="server">
                                    <asp:HyperLink ID="hlupdate" runat="server" NavigateUrl='<%#"~/QC_Data/QC_Inspection_Add.aspx?ACTION=UPDATE&NUM=0&ID="+Eval("AFI_ID") %>'>
                                        <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        修改</asp:HyperLink>
                                </asp:Panel>
                                <asp:Panel ID="Panelpaojian" runat="server" Visible="False">
                                    <asp:HyperLink ID="hlupdate1" runat="server" NavigateUrl='<%#"~/QC_Data/QC_Inspection_Add.aspx?ACTION=UPDATE&NUM=1&ID="+Eval("AFI_ID") %>'>
                                        <asp:Image ID="Image4" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        再次报检</asp:HyperLink>
                                </asp:Panel>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="详细信息" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server">
                                    <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                    详细信息</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="质检" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Panel ID="Panelzhijian" runat="server">
                                    <asp:HyperLink ID="hykzhijian1" runat="server" NavigateUrl='<%#"~/QC_Data/QC_Inspection_Add.aspx?ACTION=INSPEC&&ID="+Eval("AFI_ID")%>'>
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        质检</asp:HyperLink>
                                </asp:Panel>
                                <asp:Panel ID="Panelxiugai" runat="server" Visible="false">
                                    <asp:HyperLink ID="hykupdate" runat="server" NavigateUrl='<%#"~/QC_Data/QC_Inspection_Add.aspx?ACTION=INUPDATE&NUM=0&ID="+Eval("AFI_ID")%>'>
                                        <asp:Image ID="Image6" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        修改质检</asp:HyperLink>
                                </asp:Panel>
                                <asp:Panel ID="Panelzaici" runat="server" Visible="false">
                                    <asp:HyperLink ID="hykzhijian2" runat="server" NavigateUrl='<%#"~/QC_Data/QC_Inspection_Add.aspx?ACTION=INUPDATE&NUM=1&ID="+Eval("AFI_ID")%>'>
                                        <asp:Image ID="Image5" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                        重新质检</asp:HyperLink>
                                </asp:Panel>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="修改审核" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:HyperLink ID="hykqqsh" runat="server" Enabled="false" NavigateUrl='<%#"~/QC_Data/QC_ZJXGSH.aspx?ACTION=add&AFIID="+Eval("AFI_ID")%>'>
                                  <asp:Image ID="Image7" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />
                                 <asp:Label ID="lb1" runat="server" Text="修改审核"></asp:Label>
                                 </asp:HyperLink>
                                 <asp:HiddenField ID="issh" runat="server" Value='<%#Eval("AFI_ISSH")%>' />
                                 <asp:HiddenField ID="shjg" runat="server" Value='<%#Eval("AFI_SHJG")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                    没有任务!</asp:Panel>
                <div style="text-align: left; padding-top: 5px; padding-left: 15px">
                    <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" /></div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
        <!--box-outer END -->
    </div>
    <!--box-wrapper END -->
</asp:Content>
