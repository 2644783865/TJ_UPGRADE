<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_MpMoreCreate.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MpMoreCreate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">生成材料计划
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="100%">
             <tr>
               <td align="right">
                   <input id="btnMp_clkCreate" type="button" value="生成材料计划" onclick="return MpCreate();" />
                   <asp:Button ID="btnMpCreate" runat="server" CssClass="hidden"  OnClick="btnMpCreate_OnClick"  Text="生成材料计划" />&nbsp;&nbsp;&nbsp;&nbsp;
               </td>
             </tr>
           </table>
    <script language="javascript" type="text/javascript">
      function MpCreate()d
      {
         var yes=confirm("确认以下记录全部生成计划吗？");
         if(yes)
         {
            document.getElementById("<%=btnMpCreate.ClientID%>").click();
            document.getElementById("btnMp_clkCreate").disabled=true;
            return true;
         }
         else
         {
            return false;
         }
      }
    
    </script>
       </div>
     </div>
    </div>    
    <div class="box-wrapper">
        <div class="box-outer">
        
    <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
        <hr style="width:100%; height:0.1px; color:Blue;" />没有记录!</asp:Panel>
     <yyc:SmartGridView ID="GridView2"  width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
             <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfMPtate" Value='<%#Eval("BM_MPSTATE").ToString() %>' runat="server" />
                                        <asp:HiddenField ID="hdfMPChg" Value='<%#Eval("BM_MSSTATUS").ToString() %>' runat="server" />
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BM_TUHAO" HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_CHANAME" HeaderText="材料名称" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAGUIGE" HeaderText="材料规格" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_PNUMBER" HeaderText="计划数量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAUNITWGHT" DataFormatString="{0:F2}" HeaderText="材料单重(kg)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALWGHT" DataFormatString="{0:F2}" HeaderText="材料总重(kg)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MAWIDTH" DataFormatString="{0:F2}" HeaderText="宽度(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MPMY" DataFormatString="{0:F2}" HeaderText="计划面域(m2)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_MATOTALLGTH" DataFormatString="{0:F2}" HeaderText="材料总长(mm)"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BM_THRYWGHT" DataFormatString="{0:F2}" HeaderText="理论重量"
                                    HeaderStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                             
                                <asp:BoundField HeaderText="是否定尺" HeaderStyle-Wrap="false" DataField="BM_FIXEDSIZE"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BM_STANDARD" HeaderText="国标">
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </asp:BoundField>
                               
                                <asp:TemplateField HeaderText="变更状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMPChangeState" CssClass="notbrk" runat="server" Text='<%#Eval("BM_MPSTATUS").ToString()=="1"?"删除":Eval("BM_MPSTATUS").ToString()=="2"?"增加":Eval("BM_MPSTATUS").ToString()=="3"?"修改":"正常" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAllbeizhu" CssClass="notbrk" runat="server" Text='<%#Eval("BM_ALLBEIZHU") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
       <uc1:UCPaging ID="UCPaging1" runat="server" />

        </div>
    </div>    
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 50%; right:45%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>    
</asp:Content>
