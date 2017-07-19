<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_ShowReplaceDetail.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_ShowReplaceDetail" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看替换物料编码信息</title>
     <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
        function openLink(url) 
        {
            var date=new Date();
            var time=date.getTime();
            var returnVlue=window.showModalDialog("TM_Original_Data_Operate.aspx?NoUse="+time+"&register=" + url,'',"dialogHeight:700px;dialogWidth:700px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
            //下查材料计划
    function MP_DownWardQuery(xuhao)
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_Mp_DownWardQuery.aspx?NoUse="+time+"&xuhao_engid=" + encodeURIComponent(xuhao),'',"dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
    //下查明细
    function MS_DownWardQuery(xuhao)
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_Ms_DownWardQuery.aspx?NoUse="+time+"&xuhao_engid_table=" + encodeURIComponent(xuhao),'',"dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
    //下查外协计划
    function OUT_DownWardQuery(xuhao)
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_Out_DownWardQuery.aspx?NoUse="+time+"&xuhao_engid=" + encodeURIComponent(xuhao),'',"dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
    //下查代用
    function MarReplace_DownWardQuery(xuhao)
    {
        var date=new Date();
        var time=date.getTime();
        var returnVlue=window.showModalDialog("TM_MarReplaceView.aspx?NoUse="+time+"&xuhao_engid_marid_table=" + encodeURIComponent(xuhao),'',"dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
    }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="100%">
             <tr>
               <td style="width:30%">任务号:
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
             <td style="width:30%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:30%">设备名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
              
            </td>
            <td align="right">
                <input id="btnClose" type="button" onclick="window.close();" value="关 闭" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
             </tr>
           </table>
       </div>
     </div>
   </div>
    <div class="box-wrapper" >
      <div class="box-outer">
<cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0" >
<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="原始数据可替换项" TabIndex="0">
<ContentTemplate>
    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center" Font-Size="Large">
        没有记录!</asp:Panel>
    <yyc:SmartGridView ID="SmartGridView1" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" DataKeyNames="BM_XUHAO" 
        onrowdatabound="GridView1_RowDataBound" AllowPaging="False">
        <RowStyle BackColor="#EFF3FB"  Wrap="false" />
        <HeaderStyle Wrap="false" />
        <Columns>
            <asp:TemplateField HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete()" CssClass="checkBoxCss"/>
                    <input id="Hidden1" type="hidden" runat="server" />
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                    <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_OSSTATE").ToString()+"-"+Eval("BM_OSSTATUS").ToString()+"-"+Eval("BM_CONDICTIONATR").ToString()+"-"+Eval("BM_MARID").ToString()+"-"+Eval("BM_WMARPLAN").ToString() %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="W" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="BM_MSXUHAO"  HeaderText="明细序号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" HeaderStyle-Wrap="false" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO"  HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID"   HeaderText="物料编码"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU"   HeaderText="总序"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME"   HeaderText="中文名称"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ENGSHNAME"   HeaderText="英文名称" Visible="false" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE"   HeaderText="规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"   HeaderText="库"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>            
            <asp:BoundField DataField="BM_BEIZHUATR"   HeaderText="条件属性"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KEYCOMS"   HeaderText="关键部件"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="委外部门" HeaderStyle-Wrap="false">
            <ItemTemplate>
                <asp:Label ID="condiction" runat="server"  CssClass="notbrk"
                Text='<%# Eval("BM_CONDICTIONATR").ToString()=="03"?"技术部":Eval("BM_CONDICTIONATR").ToString()=="06"?"采购部":"" %>'></asp:Label>                           
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MANAME"   HeaderText="材料名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE"   HeaderText="材料规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"   DataFormatString="{0:F2}" HeaderText="宽度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"   DataFormatString="{0:F2}" HeaderText="理论重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT"   DataFormatString="{0:F2}" HeaderText="材料单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"   DataFormatString="{0:F2}" HeaderText="材料总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH"   DataFormatString="{0:F2}" HeaderText="材料总长(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MABGZMY"   DataFormatString="{0:F2}" HeaderText="面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY"   HeaderText="材质"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER"   HeaderText="单台数量|总数量|计划数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT"   DataFormatString="{0:F2}" HeaderText="单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT"   DataFormatString="{0:F2}" HeaderText="总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUUNITWGHT"   DataFormatString="{0:F2}" HeaderText="图纸上单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUTOTALWGHT"   DataFormatString="{0:F2}" HeaderText="图纸上总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE"   HeaderText="毛坯形状"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE"   HeaderText="毛坯状态"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS"   HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_STANDARD"   HeaderText="国标"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px"  TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="原始数据不可替换项" TabIndex="1">
<ContentTemplate>
    <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
        没有记录!</asp:Panel>
    <yyc:SmartGridView ID="SmartGridView2" width="100%" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" DataKeyNames="BM_XUHAO" 
        onrowdatabound="GridView2_RowDataBound" AllowPaging="False">
        <RowStyle BackColor="#EFF3FB"  Wrap="false" />
        <HeaderStyle Wrap="false" />
        <Columns>
            <asp:TemplateField HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" onclick="chkDelete()" CssClass="checkBoxCss"/>
                    <input id="Hidden1" type="hidden" runat="server" />
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                    <asp:HiddenField ID="hdfOrgState" runat="server" Value='<%#Eval("BM_MPSTATE").ToString()+"-"+Eval("BM_MPSTATUS").ToString()+"-"+Eval("BM_MSSTATE").ToString()+"-"+Eval("BM_MSSTATUS").ToString()+"-"+Eval("BM_OSSTATE").ToString()+"-"+Eval("BM_OSSTATUS").ToString()+"-"+Eval("BM_CONDICTIONATR").ToString()+"-"+Eval("BM_MARID").ToString()+"-"+Eval("BM_WMARPLAN").ToString() %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="C" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="Z" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField HeaderText="W" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="BM_MSXUHAO"  HeaderText="明细序号"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" HeaderStyle-Wrap="false" >
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUHAO"  HeaderText="图号(标识号)" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MARID"   HeaderText="物料编码"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU"   HeaderText="总序"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Left" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME"   HeaderText="中文名称"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ENGSHNAME"   HeaderText="英文名称" Visible="false" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_GUIGE"   HeaderText="规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KU"   HeaderText="库"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>            
            <asp:BoundField DataField="BM_BEIZHUATR"   HeaderText="条件属性"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_KEYCOMS"   HeaderText="关键部件"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="委外部门" HeaderStyle-Wrap="false">
            <ItemTemplate>
                <asp:Label ID="condiction" runat="server"  CssClass="notbrk"
                Text='<%# Eval("BM_CONDICTIONATR").ToString()=="03"?"技术部":Eval("BM_CONDICTIONATR").ToString()=="06"?"采购部":"" %>'></asp:Label>                           
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="BM_MANAME"   HeaderText="材料名称" HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE"   HeaderText="材料规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:F2}" HeaderText="长度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"   DataFormatString="{0:F2}" HeaderText="宽度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"   DataFormatString="{0:F2}" HeaderText="理论重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAUNITWGHT"   DataFormatString="{0:F2}" HeaderText="材料单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"   DataFormatString="{0:F2}" HeaderText="材料总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALLGTH"   DataFormatString="{0:F2}" HeaderText="材料总长(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MABGZMY"   DataFormatString="{0:F2}" HeaderText="面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY"   HeaderText="材质"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER"   HeaderText="单台数量|总数量|计划数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_UNITWGHT"   DataFormatString="{0:F2}" HeaderText="单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TOTALWGHT"   DataFormatString="{0:F2}" HeaderText="总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUUNITWGHT"   DataFormatString="{0:F2}" HeaderText="图纸上单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUTOTALWGHT"   DataFormatString="{0:F2}" HeaderText="图纸上总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASHAPE"   HeaderText="毛坯形状"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MASTATE"   HeaderText="毛坯状态"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS"   HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_STANDARD"   HeaderText="国标"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_NOTE"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px"  TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPaging2" runat="server" />
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer>
      </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 50%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
    </form>
</body>
</html>
