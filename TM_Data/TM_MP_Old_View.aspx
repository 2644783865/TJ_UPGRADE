<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_Old_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Old_View" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">

<script language="javascript" type="text/javascript">
    function ShowDetail(index)
    {
        var newindex=index.substring(0,index.indexOf("-"));
        window.showModalDialog('TM_MPBefore.aspx?LotNum=<%=lbllotNum.Text %>&NewIndex='+newindex,'',"dialogHeight:300px;dialogWidth:1000px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
    }
</script>
    材料需用计划(<asp:Label ID="lbllotNum" runat="server"></asp:Label>)
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>    
<div class="box-wrapper">
  <div class="box-inner" >
        <div class="box_right" style="height:10px;">
            <div class="box-title">
            </div>
        </div>
    </div>
    <div class="box-outer">
    <div style="width:100%;vertical-align:bottom; text-align:center;font-size:20px;">变更后计划明细</div>
            <br />
            <yyc:SmartGridView ID="GridView1" OnRowCreated="GridView1_RowCreated" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="变更信息" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                                <asp:HyperLink ID="hplBeforeChg" CssClass="hand" ToolTip='<%#Eval("MP_NEWXUHAO")+"-点击查看变更前信息" %>' onclick="ShowDetail(this.title);" runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />                                <asp:Label ID="lblChgState" runat="server" Text='<%#Eval("MP_CHANGESTATE").ToString()=="1"?"删除":Eval("MP_CHANGESTATE").ToString()=="2"?"增加":Eval("MP_CHANGESTATE").ToString()=="3"?"修改":"" %>'></asp:Label>
                                </asp:HyperLink>                       
                         <cc1:PopupControlExtender
                            ID="PopupControlExtender1" 
                            runat="server" 
                            DynamicServiceMethod="GetMpDynamicContent"
                            DynamicContextKey='<%# Eval("MP_NEWXUHAO") %>'
                            DynamicControlID="Panel1"
                            TargetControlID="hplBeforeChg"
                            PopupControlID="Panel1"
                            Position="Right" OffsetY="25">
                        </cc1:PopupControlExtender>                   
                     </ItemTemplate>
                    </asp:TemplateField> 
                   <asp:BoundField DataField="MP_NEWXUHAO" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_MARID" HeaderText="材料ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_WEIGHT" HeaderText="重量"  DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_KEYCOMS" HeaderText="关键部件" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_FIXEDSIZE" HeaderText="是否定尺" ItemStyle-HorizontalAlign="Center" />              
                 </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <%--<FixRowColumn FixRowType="Header,Pager" TableHeight="480px" TableWidth="100%" />--%>
            </yyc:SmartGridView>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <br />
        <div style="width:100%;vertical-align:bottom; text-align:center; font-size:20px;">本批变更新增/减少计划量</div>
         <asp:Panel ID="NoDataPanel" runat="server"><div style="text-align:center; font-size:medium;"><br />没有记录!</div></asp:Panel>
                    <yyc:SmartGridView ID="GridView2" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                                    <asp:BoundField DataField="MP_TUHAO" HeaderText="图号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_MARID" HeaderText="材料ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="长度(mm)" DataField="MP_LENGTH" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="宽度(mm)" DataField="MP_WIDTH" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="是否定尺">
                                      <ItemTemplate>
                                         <asp:Label ID="lblsfdc" runat="server" Text='<%#Eval("MP_FIXEDSIZE")%>'></asp:Label> 
                                      </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="关键部件">
                                       <ItemTemplate>
                                             <asp:Label ID="lblkey" runat="server" Text='<%#Eval("MP_KEYCOMS")%>'></asp:Label> 
                                      </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_WEIGHT" HeaderText="重量" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_USAGE" HeaderText="用途" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_ENVREFFCT" HeaderText="环保影响" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="MP_TRACKNUM" HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />    
                                    <asp:BoundField DataField="MP_MASHAPE" HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />                 </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <%--<FixRowColumn FixRowType="Header,Pager" TableHeight="480px" TableWidth="100%" />--%>
            </yyc:SmartGridView>
        </div>
    </div>
</asp:Content>
