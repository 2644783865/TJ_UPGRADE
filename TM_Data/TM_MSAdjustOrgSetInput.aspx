<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MSAdjustOrgSetInput.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.master" Inherits="ZCZJ_DPF.TM_Data.TM_MSAdjustOrgSetInput" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="RightContentTitlePlace">成套设备拆分
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script language="javascript" type="text/javascript">
function okay() 
{
    window.parent.document.getElementById('ButtonViewDone').click();
}

function cancel() 
{
   window.parent.document.getElementById('ButtonViewCancel').click();
}

//查看汇总
function View_Split()
{
    var date=new Date();
    var time=date.getTime();
    window.open("TM_MSAdjustOrgSetInputView.aspx?NoUse="+time);
}
</script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <div class="box-inner">
  <div class="box_right">
    <div class="box-title">
      <table width="100%">
       <tr>
         <td>台数:<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label></td>
         <td align="right">
            <strong>拆分总序对应<span style="color:Red;">序号</span>:</strong><asp:TextBox ID="txtXuHao" runat="server" onfocus="this.select();" AutoPostBack="true" OnTextChanged="txtXuHao_OnTextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;</td>
             <td align="right">拆分条数:
               <asp:DropDownList ID="ddlSplitNums" AutoPostBack="true" OnSelectedIndexChanged="ddlSplitNums_OnSelectedIndexChanged" runat="server">
               </asp:DropDownList>
             </td>
          <td>
           <asp:Button ID="btnQuery" runat="server" OnClientClick='return confirm("页面所有信息将重置,确认吗？")' OnClick="btnQuery_Click" Text="查 询" /> &nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnClear" runat="server" OnClick="btnClear_OnClick" Text="清空重填" />&nbsp;&nbsp;&nbsp;
        </td>
        <td align="right">
          <span style="color:Red; font-size:larger;">如果要取消拆分，请点击这里关闭&nbsp;&nbsp;</span><a onclick="cancel();" style="font-family: Verdana, Helvetica, Arial, sans-serif;background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 3px;" title="关闭">X</a>
        </td>
        </tr>
     </table>  
    </div>
    </div>
  </div>
    <div class="box-wrapper">
      <div class="box-outer">
      <asp:Panel ID="NoDataPanel" runat="server"><span style="color:Red;">没有记录！</span>
                </asp:Panel>
          <asp:Label ID="lblErrorText" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
            <asp:GridView ID="grvOrgData" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="序号" DataField="BM_XUHAO" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="图号(标识号)" DataField="BM_TUHAO" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="总序" DataField="BM_ZONGXU" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="中文名称" DataField="BM_CHANAME" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="物料编码" DataField="BM_MARID" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="规格" DataField="BM_MAGUIGE" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材质" DataField="BM_MAQUALITY" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="单台数量|总数量|计划数量" DataField="NUMBER" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="实际单重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_UNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="实际总重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_TOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料单重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_MAUNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料总重(kg)" ItemStyle-Wrap="false" DataFormatString="{0:f4}" HeaderStyle-Wrap="false"  DataField="BM_MATOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="长度(mm)" ItemStyle-Wrap="false" DataFormatString="{0:f2}" HeaderStyle-Wrap="false"  DataField="BM_MALENGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="宽度(mm)" ItemStyle-Wrap="false" DataFormatString="{0:f2}" HeaderStyle-Wrap="false"  DataField="BM_MAWIDTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="总长(mm)" ItemStyle-Wrap="false" DataFormatString="{0:f2}" HeaderStyle-Wrap="false"  DataField="BM_MATOTALLGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="面域|计划面域(m&sup2;)" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  DataField="BM_MY" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_STANDARD" ItemStyle-Wrap="false" HeaderText="国标" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_PROCESS" ItemStyle-Wrap="false" HeaderText="工艺流程" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_KEYCOMS" ItemStyle-Wrap="false" HeaderText="关键部件" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_ISMANU" ItemStyle-Wrap="false" HeaderText="体现明细" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_NOTE" ItemStyle-Wrap="false" HeaderText="备注" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_MANAME" ItemStyle-Wrap="false" HeaderText="物料名称" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_THRYWGHT" ItemStyle-Wrap="false" HeaderText="理论重量" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="BM_MASHAPE" ItemStyle-Wrap="false" HeaderText="毛坯形状" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_FIXEDSIZE" ItemStyle-Wrap="false" HeaderText="是否定尺" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_MAUNIT" ItemStyle-Wrap="false" HeaderText="单位" HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                      <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                     <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                     <EditRowStyle BackColor="#2461BF" />
                     <AlternatingRowStyle BackColor="White" /> 
                </asp:GridView>
             <br />
          <div style="text-align:left; width:100%;">&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_OnClick" Text="该记录能否拆分检查" />&nbsp;&nbsp;&nbsp;
           <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_OnClick" Visible="false"><asp:Image ID="Image11" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2" 
                    align="absmiddle" runat="server" />不可拆分项查看</asp:LinkButton>&nbsp;&nbsp;&nbsp;
           <asp:LinkButton ID="LinkButton2" runat="server"  OnClick="LinkButton2_OnClick"><asp:Image ID="Image1" ImageUrl="~/Assets/icon-fuction/139.gif" border="0" hspace="2" 
                    align="absmiddle" runat="server" />拆分项全部信息</asp:LinkButton>
           <asp:Button ID="btnSplit" runat="server" Visible="false" OnClick="btnSplit_OnClick"  OnClientClick="return confirm('确认提交拆分吗？');" Text="开始拆分" />&nbsp;&nbsp;&nbsp;<span style="color:Red;">拆分为物料,原始记录如已提交计划,则原始拆分记录将保留！！！</span></div>  <hr />  
          <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" >
            <RowStyle BackColor="#EFF3FB" />
               <Columns>
               <asp:TemplateField HeaderText="行号" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                <ItemTemplate>
                <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="序号"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
               <ItemTemplate>
                   <asp:TextBox ID="txtNewXuhao" runat="server"  onfocus="this.select();" Text='<%#Eval("BM_XUHAO") %>'></asp:TextBox></ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="单台数量"  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
               <ItemTemplate>
                   <asp:TextBox ID="txtNewShuling" runat="server" onfocus="this.select();"  onblur='var pattem=/^\d+?$/; if(!pattem.test(this.value)){alert("请输入正确的数值！！！");this.value="1";}'  Text='<%#Eval("BM_SINGNUMBER") %>' ></asp:TextBox></ItemTemplate>
               </asp:TemplateField>
               </Columns>
               <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
               <EditRowStyle BackColor="#2461BF" />
               <AlternatingRowStyle BackColor="White" /> 
          </asp:GridView>
          <asp:Table ID="Table1" runat="server">
          </asp:Table>
      </div>
    </div>             
</asp:Content>
