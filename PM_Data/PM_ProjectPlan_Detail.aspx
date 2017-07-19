<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="PM_ProjectPlan_Detail.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_ProjectPlan_Detail" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细表
    </asp:Content>
        
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
   <script src="../JS/PDMN.js" type="text/javascript" charset="GB2312"></script>
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div style="height:6px" class="box_top"></div>
<div class="box-outer">
    <table width="100%">
    <tr>
        <td style=" font-size:large; text-align:center;" colspan="8">制作明细</td>
    </tr>
    <tr>
        <td style="width:8%" align="right">任务号:</td>
        <td style="width:15%"><asp:Label ID="tsa_id" runat="server" Width="100%"/></td>
        <td style="width:8%" align="right">合同名称:</td>
        <td style="width:14%"><asp:Label ID="lab_proname" runat="server" Width="100%"/></td>
        
        <td style="width:8%" align="right">设备名称:</td>
        <td style="width:14%"><asp:Label ID="lab_engname" runat="server" Width="100%"/></td>
        <td style="width:8%" align="right">批号:</td>
        <td style="width:25%"><asp:Label ID="ms_no" runat="server" Width="100%"/></td>
    </tr>
    <tr>
        <td style="width:8%" align="right">编制人:</td>
        <td colspan="1"><asp:Label ID="lbltcname" runat="server" Width="100%"/></td>
        <td style="width:8%" align="right">编制日期:</td>
        <td style="width:14%"><asp:Label ID="txt_plandate" runat="server" Width="100%"/></td> 
        <td id="endtime" style="width:8%" align="right"><asp:Label ID="time" Text="交货期:" runat="server" Width="100%" /></td>
        <td style="width:14%" ><asp:Label ID="endTime" runat="server" Width="100%"/></td>  
        <td colspan="2" align="center">
           <asp:Button ID="btnproplan" runat="server" OnClick="btnproplan_Click"  Text="制作项目计划" />&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    </table>
</div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  >
                     <ItemTemplate>
                        <asp:CheckBox ID="chbxcheck" runat="server" /> 
                     </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
               
               
                <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_NAME" HeaderText="名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_NUM" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_TUWGHT" HeaderText="图纸单重(kg)" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_TUTOTALWGHT" HeaderText="图纸总重(kg)" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_MASHAPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_LEN" HeaderText="长度" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_WIDTH" HeaderText="宽度" ItemStyle-HorizontalAlign="Center" />
                 <asp:BoundField DataField="MS_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
               <%-- <asp:BoundField DataField="MS_MAUNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="MS_XIALIAO" HeaderText="下料" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_PROCESS" HeaderText="加工" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_WAIXINGCHICUN" HeaderText="外形尺寸" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MS_KU" HeaderText="入库类别" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="主键" FooterStyle-CssClass="hidden " ItemStyle-CssClass="hidden " HeaderStyle-CssClass="hidden ">
                    <ItemTemplate>
                        <asp:Label ID="lblmsid" runat="server" Text='<%# Eval("MS_ID") %>'></asp:Label>
                        <asp:Label ID="lblmarid" runat="server" Text='<%#Eval("MS_MARID") %>'></asp:Label>
                        <asp:Label ID="lblkeycoms" runat="server" Text='<%#Eval("MS_KEYCOMS") %>'></asp:Label>
                        <asp:Label ID="lblpdstate" runat="server" Text='<%#Eval("MS_PDSTATE") %>'></asp:Label>
                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("MS_STATUS") %>'></asp:Label>
                       
                        <asp:Label ID="lblwaixie" runat="server" Text='<%#Eval("MS_PLAN") %>'></asp:Label>
                        
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />  
             <FixRowColumn FixRowType="Header,Pager"  TableHeight="500px" TableWidth="100%" FixColumns="0,1" />                     
        </yyc:SmartGridView> 
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <div>
                            <table width="100%" style="text-align:center">
                                <tr>
                                    <td>
                                        负责人:
                                        <asp:DropDownList ID="cob_fuziren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td>
                                        申请人:
                                        <asp:DropDownList ID="cob_sqren" runat="server" Width="120px">
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td>
                                        制单人:<asp:TextBox ID="TextBoxexecutor" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="TextBoxexecutorid" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div>
        <asp:HiddenField ID="hfpldetail" runat="server" />
        <asp:Label ID="ControlFinder" runat="server" Visible="false" ></asp:Label>
    </div> 
    
    </asp:Content>
