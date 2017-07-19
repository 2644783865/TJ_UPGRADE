<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_OrgDataImportAll.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_OrgDataImportAll" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/UserInputZongxu.ascx" tagname="UserInputZongxu" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<script language="javascript" type="text/javascript">
  function ShowSource(copytype,taskid,tablename,fx)
  {
     var date=new Date();
     var time=date.getTime();
     window.open("TM_OrgDataInputAll_Show.aspx?CopyType=" + copytype + "&TaskID=" + taskid + "&TableName=" + tablename + "&XuHao=" + fx + "&No=" +time);
  }
  
  function ShowTarget(copytype,sc_taskid,sc_tablename,sc_fx,sc_arry)
  {
     var date=new Date();
     var time=date.getTime();
     window.open("TM_OrgDataInputAll_TargetShow.aspx?CopyType=" + copytype + "&TaskID=" + sc_taskid + "&TableName=" + sc_tablename + "&XuHao=" + sc_fx + "&No=" +time);
  }
  
  function ShowDetail(copytype,tg_taskid,tg_tablename,tg_fx,sc_taskid,sc_tablename,sc_fx,sc_array)
  {
     var date=new Date();
     var time=date.getTime();
     window.open("TM_OrgDataInputAll_DetailShow.aspx?CopyType=" + copytype + "&Sc_TaskID=" + sc_taskid + "&Sc_TableName=" + sc_tablename + "&Sc_XuHao=" + sc_fx + "&Tg_TaskID=" + tg_taskid + "&Tg_TableName=" + tg_tablename + "&Tg_XuHao=" + tg_fx + "&No=" +time+"&array="+sc_array);
  }
  
  function NodeCheck() 
  {   
    var o = window.event.srcElement;
    if (o.tagName == "INPUT" && o.type == "checkbox") //点击treeview的checkbox是触发  
    {
       __doPostBack("TreeViewMp","");
////////////        var d = o.id; //获得当前checkbox的id;  
////////////        var e = d.replace("CheckBox", "Nodes"); //通过查看脚本信息,获得包含所有子节点div的id  
////////////        var div = window.document.getElementById(e); //获得div对象  
////////////        if (div != null)  //如果不为空则表示,存在自节点  
////////////        {
////////////            var check = div.getElementsByTagName("INPUT"); //获得div中所有的已input开始的标记  
////////////            for (i = 0; i < check.length; i++) 
////////////            {
////////////                if (check[i].type == "checkbox") //如果是checkbox  
////////////                {
////////////                    check[i].checked = o.checked; //字节点的状态和父节点的状态相同,即达到全选  
////////////                }
////////////            }
////////////        }
////////////        
////////////        //点子节点的时候,使父节点的状态改变,即不为全选
////////////        var divid = o.parentElement.parentElement.parentElement.parentElement.parentElement; //子节点所在的div      
////////////        var id = divid.id.replace("Nodes", "CheckBox"); //获得根节点的id
////////////        var div_father=window.document.getElementById(id);
////////////        while(div_father!=null)
////////////        {
////////////            var checkbox = divid.getElementsByTagName("INPUT"); //获取所有子节点数
////////////            var s = 0;
////////////            for (i = 0; i < checkbox.length; i++) 
////////////            {
////////////                if (checkbox[i].checked)  //判断有多少子节点被选中  
////////////                {
////////////                    s++;
////////////                }
////////////            }
////////////            if (s == checkbox.length)  //如果全部选中 或者 选择的是另外一个根节点的子节点 ，  
////////////            {                               //    则开始的根节点的状态仍然为选中状态  
////////////                window.document.getElementById(id).checked = true;
////////////            }
////////////            else 
////////////            {                               //否则为没选中状态  
////////////                window.document.getElementById(id).checked = false;
////////////            }
////////////            divid =window.document.getElementById(id).parentElement.parentElement.parentElement.parentElement.parentElement; //子节点所在的div     
////////////            id = divid.id.replace("Nodes", "CheckBox"); //获得根节点的id
////////////            div_father=window.document.getElementById(id);
////////////         }
      }
  }
</script>
  <table width="100%" >
   <tr>
     <td align="left">导入数据至目标工程</td>
   </tr>
   </table>
</asp:Content>  

<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent"  runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
 <table width="98%">
       <tr>
       <td style="font-weight:bold;">目标工程</td>
        <td align="right">项目名称:</td>
        <td align="left"><asp:Label ID="lblProjName" runat="server" Text="Label"></asp:Label></td>
        <td align="right">设备名称:</td>
        <td align="left"><asp:Label ID="lblEngName"  runat="server" Text="Label"></asp:Label>
           <asp:HiddenField ID="hdfType" runat="server" />
           <asp:HiddenField ID="hdfEngid" runat="server" />
           <asp:HiddenField ID="hdfProid" runat="server" />
        </td>
        <td></td>
        <td></td>
      </tr>
  <tr>
  <td  style="font-weight:bold;">数&nbsp;据&nbsp;源</td>
  <td align="right">项目名称:</td>
    <td style="width:18%; height:42px" align="left" valign="top"><cc1:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px"
        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
         onselectedindexchanged="ddlProName_SelectedIndexChanged"> </cc1:ComboBox></td>
 <td align="right">设备名称:</td>
    <td style="width:18%; height:42px" align="left" valign="top"><cc1:ComboBox ID="ddlEngName" runat="server" AutoPostBack="true" Height="15px"
     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"  onselectedindexchanged="ddlEngName_SelectedIndexChanged">
   </cc1:ComboBox></td>
  <td align="center"></td>
  </tr>
</table>

  <table id="tb_all" runat="server" visible="false">
    <tr>
    <td>
      <table width="100%">
       <tr>
      
       
        <td style=" display:none; width:70px;" >复制下级(不使用功能):</td>
        <td><asp:DropDownList ID="ddlFZ" runat="server"  AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlZF_OnSelectedIndexChanged">
            <asp:ListItem  Text="是" Value="Y" Selected="True"></asp:ListItem>
            <asp:ListItem  Text="否" Value="N"></asp:ListItem>
          </asp:DropDownList></td>
           <td>
       
       </td>
        <td align="center"><asp:Button ID="btnAddFX" runat="server" Width="100px" Text="添加待复制父序(每次添加一项，不使用此功能了)" Visible="false" OnClick="btnAddFX_OnClick" />
        &nbsp;&nbsp;&nbsp;
        
         导入台数:<asp:TextBox ID="lblNumber" runat="server" Width="40px" onblur='var pattem=/^\d{1,3}?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1";this.foucs();}'></asp:TextBox>
        &nbsp;<asp:Button ID="btnExportAll" runat="server" OnClientClick="return confirm('确认导入吗？\r\r提示:导入请仔细确认各项导入详细！！！');" OnClick="btn_ImportAll" Text="开始导入" />
        </td>
       </tr>
      </table>
    </td>
    <td></td>
    </tr>
    <tr>
      <td><span>
      提示:1、请在下拉框中选择【待复制父序】；2、多个【复制后父序】间不能存在归属关系；3、如果【复制后父序】存在部分记录，则数据源中对应的记录将不复制；<br />4、父序替换关系，如数据源1.3.1>目标工程1.5，则数据源下级1.3.1.1>目标工程1.5.1。</span>
      <asp:Table ID="tb1" runat="server">
      </asp:Table></td>
      <td style="border: #B9D3EE 2px solid; vertical-align:top;"><br />
         <div>&nbsp;图号(标识)中<asp:TextBox ID="txtOldTu_1"  Width="60px" onfocus="this.select();" runat="server"></asp:TextBox>替换为
         <asp:TextBox ID="txtNewTu_1" Width="60px" onfocus="this.select();" runat="server"></asp:TextBox>&nbsp;</div><br />
         <div>&nbsp;图号(标识)中<asp:TextBox ID="txtOldTu_2" onfocus="this.select();"  Width="60px" runat="server"></asp:TextBox>替换为
         <asp:TextBox ID="txtNewTu_2"  Width="60px" onfocus="this.select();" runat="server"></asp:TextBox>&nbsp;</div><br />
      </td>
    </tr>
  </table>
          <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" Font-Size="Large">
    没有记录!</asp:Panel>
        <yyc:SmartGridView ID="GridView1" CssClass="toptable grid" runat="server"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="BM_XUHAO"  AllowPaging="False">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Width="10px"  CssClass="checkBoxCss"/>
                </ItemTemplate>
                <ItemStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
           
         
            <asp:BoundField DataField="BM_TUHAO"  HeaderText="图号(标识号)"  HeaderStyle-Wrap="false">
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
              
         
            <asp:BoundField DataField="BM_MAGUIGE"   HeaderText="材料规格"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MAQUALITY"   HeaderText="材质"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
            <asp:BoundField DataField="BM_MALENGTH" DataFormatString="{0:N2}" HeaderText="长度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAWIDTH"   DataFormatString="{0:N2}" HeaderText="宽度(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
                 <asp:BoundField DataField="BM_NOTE"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
            </asp:BoundField>
             <asp:BoundField DataField="NUMBER"   HeaderText="单台数量|总数量|计划数量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
              <asp:BoundField DataField="BM_TECHUNIT"   DataFormatString="{0:F2}" HeaderText="单位"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
              <asp:BoundField DataField="BM_YONGLIANG"   DataFormatString="{0:F2}" HeaderText="材料用量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            
              <asp:BoundField DataField="BM_MATOTALLGTH"   DataFormatString="{0:F2}" HeaderText="材料总长(mm)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MABGZMY"   DataFormatString="{0:F2}" HeaderText="面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MPMY"   DataFormatString="{0:F2}" HeaderText="计划面域(m2)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_TUUNITWGHT"   DataFormatString="{0:F2}" HeaderText="图纸单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_TUTOTALWGHT"   DataFormatString="{0:F2}" HeaderText="图纸总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" BackColor="Yellow" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MAUNITWGHT"   DataFormatString="{0:N2}" HeaderText="材料单重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MATOTALWGHT"   DataFormatString="{0:N2}" HeaderText="材料总重(kg)"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Yellow" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MASHAPE"   HeaderText="材料类别"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
               <asp:BoundField DataField="BM_XIALIAO"   HeaderText="下料方式"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_PROCESS"   HeaderText="工艺流程"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
                
            <asp:BoundField DataField="BM_ALLBEIZHU"   HeaderText="备注"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_THRYWGHT"   DataFormatString="{0:F2}" HeaderText="理论重量"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" BackColor="Silver" Wrap="false" />
            </asp:BoundField>
            
            <asp:BoundField DataField="BM_STANDARD"   HeaderText="国标"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
          
            <asp:BoundField DataField="BM_FIXEDSIZE"   HeaderText="是否定尺"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_WMARPLAN"   HeaderText="材料计划"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ISMANU"   HeaderText="制作明细"  HeaderStyle-Wrap="false">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
             </Columns>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        <FixRowColumn FixRowType="Header,Pager"  TableHeight="450px" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    <uc1:UCPaging ID="UCPaging1" runat="server" Visible="false" />
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 20%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:large;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>    
